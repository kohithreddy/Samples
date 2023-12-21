# Define variables
$organization = "<Org Name>"
$projectName = "<Project Name>"
$personalAccessToken = "<PAT TOKEN>"

$headers = @{
    Authorization = "Basic " + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":$personalAccessToken"))
    'Content-Type' = 'application/json'
}

# Get Project ID
$projectUri = "https://dev.azure.com/$organization/_apis/projects?api-version=7.1-preview.4"

try {
    $projectInfo = Invoke-RestMethod -Uri $projectUri -Method Get -Headers $headers

    if ($projectInfo.value.Count -gt 0) {
        $foundProject = $projectInfo.value | Where-Object { $_.name -eq $projectName }

        if ($foundProject -ne $null) {
            $finalId = $foundProject.id
            Write-Host "Project ID for '$projectName' is $finalId"
        } else {
            Write-Host "Project '$projectName' not found in the organization '$organization'."
        }
    } else {
        Write-Host "There are no projects in the organization '$organization'."
    }
} catch {
    Write-Host "Error: $_"
}
