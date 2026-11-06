# GitHub-hosted Agents for Azure Pipelines (Pay-As-You-Go)

Sample YAML pipelines and setup notes for running Azure Pipelines jobs on the **GitHub-hosted Agents** pool. This pool gives you larger VMs than standard Microsoft-hosted agents and bills per minute (pay-as-you-go).

> 📖 Official documentation: [GitHub-hosted agents for Azure Pipelines (pay-as-you-go) – Microsoft Learn](https://learn.microsoft.com/en-us/azure/devops/pipelines/agents/github-hosted?view=azure-devops&tabs=yaml%2Cwindows-images#use-a-github-hosted-agent)
> 📖 Sample yaml file : 
---

## Table of contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Enable the GitHub-hosted Agents pool](#enable-the-github-hosted-agents-pool)
- [Basic YAML usage](#basic-yaml-usage)
- [Available image labels](#available-image-labels)
- [Sample: multi-stage pipeline with a different OS per stage](#sample-multi-stage-pipeline-with-a-different-os-per-stage)
- [Monitoring usage and cost](#monitoring-usage-and-cost)
- [Networking and security notes](#networking-and-security-notes)
- [Comparison with other agent options](#comparison-with-other-agent-options)
- [Troubleshooting](#troubleshooting)
- [References](#references)

---

## Overview

| Item | Details |
|---|---|
| Pool name | `GitHub-hosted Agents` (created automatically when you enable the feature) |
| Billing | Per minute of job run time; the rate depends on OS and machine size |
| Free tier | **None**: every minute is billed |
| Machine sizes | Larger than standard Microsoft-hosted agents (8-core / 16-core Linux and Windows, Standard / XL macOS) |
| Infrastructure | Same infrastructure as GitHub Actions larger runners |
| VM lifecycle | New VM for each job, reimaged after the job, nothing carries over between jobs |

---

## Prerequisites

- Permission to manage **billing settings** in your Azure DevOps organization.
- An **Azure subscription** in the **same Microsoft Entra ID tenant** as the Azure DevOps organization.
- Billing set up for the organization ([Set up billing](https://learn.microsoft.com/en-us/azure/devops/organizations/billing/set-up-billing-for-your-organization-vs)).

---

## Enable the GitHub-hosted Agents pool

1. Go to **Organization settings → Billing**.
2. Make sure an Azure subscription is linked.
3. Set **Enable GitHub-hosted agents** to **On**.
4. Select **Save**.
5. Check that a new pool named **GitHub-hosted Agents** appears under **Organization settings → Agent pools**.
6. In the pool's **Security** settings, give your project and pipelines access to it.

---

## Basic YAML usage

You need **both** settings in the `pool` section:

- `name`: always `'GitHub-hosted Agents'`
- `vmImage`: one of the [image labels](#available-image-labels) below

```yaml
trigger: none

pool:
  name: 'GitHub-hosted Agents'
  vmImage: 'ubuntu-24.04-8-core'

steps:
- bash: |
    echo "Hello from a GitHub-hosted agent"
    echo "Agent : $(Agent.Name)"
    echo "Pool  : $(Agent.PoolName)"
    lsb_release -d
    echo "Cores : $(nproc)"
    free -h
  displayName: 'Show agent info'
```

> ⚠️ **Common mistake:** `vmImage: 'GitHub-hosted Agents'` is **wrong**. `GitHub-hosted Agents` is the **pool name**, not an image label.

**Classic pipelines:** choose the image from the **Agent Specification** list in the pipeline settings.

---

## Available image labels

### macOS

macOS jobs always run in the **US**, wherever your Azure DevOps organization is located.

| OS | Size | vCPU / RAM / SSD | YAML `vmImage` |
|---|---|---|---|
| macOS 26 | Standard (arm64 M1) | 3 / 7 GB / 14 GB | `macos-26-arm64` |
| macOS 26 | XLarge (arm64 M2) | 5 (+8 GPU) / 14 GB / 14 GB | `macos-26-arm64-xl` |
| macOS with Xcode 27 | Standard | 3 / 7 GB / 14 GB | `xcode-27` |
| macOS with Xcode 27 | XLarge | 5 (+8 GPU) / 14 GB / 14 GB | `xcode-27-xlarge` |

> Newer macOS images are named after the **Xcode version**, not the OS version. The underlying macOS version may change during the image's lifetime.

### Linux (x64)

| OS | Size | vCPU / RAM / SSD | YAML `vmImage` |
|---|---|---|---|
| Ubuntu 24.04 | 8-core | 8 / 32 GB / 300 GB | `ubuntu-24.04-8-core` |
| Ubuntu 24.04 | 16-core | 16 / 64 GB / 600 GB | `ubuntu-24.04-16-core` |
| Ubuntu 26.04 | 8-core | 8 / 32 GB / 300 GB | `ubuntu-26.04-8-core` |
| Ubuntu 26.04 | 16-core | 16 / 64 GB / 600 GB | `ubuntu-26.04-16-core` |

### Windows (x64)

| OS | Size | vCPU / RAM / SSD | YAML `vmImage` |
|---|---|---|---|
| Windows Server 2025 + VS 2022 | 8-core | 8 / 32 GB / 300 GB | `windows-2025-8-core` |
| Windows Server 2025 + VS 2022 | 16-core | 16 / 64 GB / 600 GB | `windows-2025-16-core` |
| Windows Server 2025 + VS 2026 | 8-core | 8 / 32 GB / 300 GB | `windows-2025-vs2026-8-core` |
| Windows Server 2025 + VS 2026 | 16-core | 16 / 64 GB / 600 GB | `windows-2025-vs2026-16-core` |

> ℹ️ Ubuntu and Windows images are being rolled out to organizations gradually. If an image isn't in your **Agent Specification** list yet, check again later.

---

## Sample: multi-stage pipeline with a different OS per stage

- The macOS, Linux and Windows stages run **in parallel** (`dependsOn: []`).
- A **Summary** stage runs afterwards and prints each stage's result, even if one of them failed (`succeededOrFailed()`).

```yaml
trigger: none

appendCommitMessageToRunName: false

stages:
# ---------------- macOS ----------------
- stage: macOS
  displayName: 'macOS 26 (ARM64)'
  dependsOn: []
  jobs:
  - job: macOSJob
    displayName: 'macOS validation'
    pool:
      name: 'GitHub-hosted Agents'
      vmImage: 'macos-26-arm64'
    steps:
    - bash: |
        echo "===== macOS JOB ====="
        echo "Agent : $(Agent.Name)"
        uname -a
        sw_vers
      displayName: 'Run validation on macOS'

# ---------------- Linux ----------------
- stage: Linux
  displayName: 'Ubuntu 24.04 (8-core)'
  dependsOn: []
  jobs:
  - job: LinuxJob
    displayName: 'Linux validation'
    pool:
      name: 'GitHub-hosted Agents'
      vmImage: 'ubuntu-24.04-8-core'
    steps:
    - bash: |
        echo "===== LINUX JOB ====="
        echo "Agent : $(Agent.Name)"
        lsb_release -d
        echo "Cores : $(nproc)"
        free -h
      displayName: 'Run validation on Linux'

# ---------------- Windows ----------------
- stage: Windows
  displayName: 'Windows Server 2025 + VS2026 (8-core)'
  dependsOn: []
  jobs:
  - job: WindowsJob
    displayName: 'Windows validation'
    pool:
      name: 'GitHub-hosted Agents'
      vmImage: 'windows-2025-vs2026-8-core'
    steps:
    - pwsh: |
        Write-Host "===== WINDOWS JOB ====="
        Write-Host "Agent : $(Agent.Name)"
        Write-Host "OS    : $($PSVersionTable.OS)"
        Write-Host "Cores : $([Environment]::ProcessorCount)"
      displayName: 'Run validation on Windows'

# ---------------- Summary ----------------
- stage: Summary
  displayName: 'Summary'
  dependsOn:
  - macOS
  - Linux
  - Windows
  condition: succeededOrFailed()
  jobs:
  - job: SummaryJob
    displayName: 'Report results'
    pool:
      vmImage: 'ubuntu-latest'   # standard Microsoft-hosted pool (no per-minute charge)
    variables:
      macOSResult:   $[ stageDependencies.macOS.macOSJob.result ]
      linuxResult:   $[ stageDependencies.Linux.LinuxJob.result ]
      windowsResult: $[ stageDependencies.Windows.WindowsJob.result ]
    steps:
    - bash: |
        echo "macOS   : $(macOSResult)"
        echo "Linux   : $(linuxResult)"
        echo "Windows : $(windowsResult)"
      displayName: 'Print stage results'
```

### Notes

- **Set the pool on each job**, not at the top of the file, so each job can use a different `vmImage`.
- **To run the stages one after another**, remove `dependsOn: []`.
- **Runtime expressions (`$[ ... ]`)** only work in `variables` and `condition`. Map stage results into job variables first, then use them in scripts as `$(var)`.
- **`succeededOrFailed()`** runs even if an earlier stage failed, but not if the run is canceled. Use `always()` to run even on cancel. See [Pipeline conditions](https://learn.microsoft.com/en-us/azure/devops/pipelines/process/conditions?view=azure-devops) and [Expressions: job status check functions](https://learn.microsoft.com/en-us/azure/devops/pipelines/process/expressions?view=azure-devops#job-status-check-functions).
- 💰 Parallel stages on the GitHub-hosted pool are billed **separately per job**.

---

## Monitoring usage and cost

**Analytics in Azure DevOps**

- Go to **Organization settings → Agent pools → GitHub-hosted Agents → Analytics**.
- Choose the agent sizes and time period you want.
- Drill down from project to pipeline to job.

**Azure Cost Management**

Filter on:

- **Meter category:** `Azure DevOps`
- **Meter subcategory:** `Azure Pipelines`
- **Unit of measure:** `1/Minute`
- Optional tags: `_organizationname_`, `_projectname_`

---

## Networking and security notes

- Traffic goes over the **public internet**. **ExpressRoute and VPN aren't supported** for reaching your corporate network.
- Agents have **no public IP addresses**.
- Each VM runs a single agent for a single organization and is reimaged after every job.
- Images are **not CIS-hardened**. For CIS-hardened images, use **self-hosted agents** or **Managed DevOps Pools**.

---

## Comparison with other agent options

| Feature | Microsoft-hosted | GitHub-hosted (PAYG) | Self-hosted |
|---|---|---|---|
| Billing | Parallel jobs (concurrency) | Per minute | Your own infrastructure |
| Free tier | Yes (limited) | No | N/A |
| Machine sizes | Standard | Larger | Custom |
| Maintenance | Automatic | Automatic | Manual |
| Corporate network access | No | No | Yes |

---

## Troubleshooting

### `No agent found in pool GitHub-hosted Agents which satisfies the specified demands: Agent.Version -gtVersion x.y.z`

Azure Pipelines adds the `Agent.Version` demand automatically, so you don't need to change it. This error means the pool couldn't start a VM for the requested image. Check these in order:

1. **Image label.** Check that `vmImage` matches a label in your **Agent Specification** list exactly. The image might not have been rolled out to your organization yet.
2. **Billing.** **Enable GitHub-hosted agents** is **On** and saved, and the linked Azure subscription is active, in the same Entra tenant, and not blocked by a spending limit.
3. **Pool access.** The project and pipeline are allowed to use the **GitHub-hosted Agents** pool (pool **Security** settings).
4. **Test a different image.** If several images all fail (for example, Linux and macOS), the cause is at the organization level (billing or provisioning), not your YAML.
5. **Escalate.** Contact support with the organization name, project, run ID, timestamp, pool, image labels you tried and the full error text.

### Pipeline runs but uses the wrong pool

- Make sure the job has `pool.name: 'GitHub-hosted Agents'`.
- Check that a job-level or stage-level `pool` isn't overriding the one you set.

---

## References

- [GitHub-hosted agents for Azure Pipelines (pay-as-you-go)](https://learn.microsoft.com/en-us/azure/devops/pipelines/agents/github-hosted?view=azure-devops&tabs=yaml%2Cwindows-images#use-a-github-hosted-agent)
- [Microsoft-hosted agents](https://learn.microsoft.com/en-us/azure/devops/pipelines/agents/hosted?view=azure-devops)
- [Pipeline conditions](https://learn.microsoft.com/en-us/azure/devops/pipelines/process/conditions?view=azure-devops)
- [Expressions](https://learn.microsoft.com/en-us/azure/devops/pipelines/process/expressions?view=azure-devops)
- [Azure DevOps pricing](https://azure.microsoft.com/pricing/details/devops/azure-devops-services/)
- [Managed DevOps Pools](https://learn.microsoft.com/en-us/azure/devops/managed-devops-pools/overview)
