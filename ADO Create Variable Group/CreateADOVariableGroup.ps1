# Define variables
$organization = "KohithFS"
$projectName = "test"
$personalAccessToken = "2twb7czbbcu7hpoxblycrmfqvih3v5nyt4qarqiwrqco7v2zaduq"
$variableGroupName ="fotrrr"
$desc = "Description of new variable group"

$variables = @{
    "Key1" = @{
        "value" = "Value1"
        "isSecret" = $false
    }
    "Key2" = @{
        "value" = "Value2"
        "isSecret" = $false
    }
}
# Convert variables to JSON
$variablesJson = $variables | ConvertTo-Json

$headers = @{
    Authorization = "Basic " + [Convert]::ToBase64String([Text.Encoding]::ASCII.GetBytes(":$personalAccessToken"))
    'Content-Type' = 'application/json'
}

# Get Project ID
$projectUri = "https://dev.azure.com/"+$organization+"/"+"_apis/projects?api-version=7.1-preview.4"
$projectInfo = Invoke-RestMethod -Uri $projectUri -Headers $headers -Method Get -ContentType "application/json;charset=utf-8"

# Display Project ID
if ($projectInfo) {
    $project = $projectInfo.value | Where-Object { $_.name -eq $projectName }
    if ($project -ne $null) {
        $finalId = $project.id
        Write-Host "Project ID for $projectName is: $finalId"
    } else {
        Write-Host "No project found with the name '$projectName'"
    }
} else {
    Write-Host "No projects retrieved from the organization '$organization'"
}

$prjreference =  @{ 
          "projectReference" = @( @{ "id"= $finalid  } )
           "name" = $projectName
                  }                 

# Create Variable Group Body
$body = @{
    description = $desc
    name = $variableGroupName
    type = "Vsts"  
    variables = $variables
    variableGroupProjectReferences = $prjreference
} | ConvertTo-Json


# Create Variable Group using Azure DevOps REST API
$uri="https://dev.azure.com/$organization/$projectName/_apis/distributedtask/variablegroups?api-version=5.1-preview.1"

# Send request to create Variable Group
$response = Invoke-RestMethod -Uri $uri -Method POST -Headers $headers -Body $body

Write-Host "Variable Group created successfully with ID $($response.id)"

