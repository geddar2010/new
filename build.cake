#addin "nuget:https://api.nuget.org/v3/index.json?package=Cake.WebDeploy"
//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument ("target", "Default");
var configuration = Argument ("configuration", "Debug");
var siteName = "betc0.a2hosted.com";
var userName = "betcahos";
var deployPassword = "Y329jhafrafr!";
var deployUrl = "https://betc0.a2hosted.com:8172/msdeploy.axd?site=betc0.a2hosted.com";

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories.
var binaryDir = Directory("./BetCore/bin") + Directory (configuration);
var objectDir = Directory("./BetCore/obj");
var publishDir = Directory("./publish");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task ("Clean")
    .Does (() => {
        CleanDirectory (binaryDir);
        CleanDirectory (objectDir);
        CleanDirectory (publishDir);
    });

Task ("Restore")
    .Does (() => 
        DotNetCoreRestore (".")
    );

Task ("Build")
    .Does (() => {
        var settings = new DotNetCoreBuildSettings{
            OutputDirectory = binaryDir
            
        };
        DotNetCoreBuild (".");
    });

Task ("Publish")
    .IsDependentOn("Build")
    .Does (() => {
        var settings = new DotNetCorePublishSettings {
        Framework = "netcoreapp2.0",
        Configuration = configuration,
        OutputDirectory = publishDir,
        Runtime = "win-x64",
		SelfContained = true
        };

        DotNetCorePublish ("BetCore", settings);
    });

Task("Deploy")
    .IsDependentOn("Publish")
    .Does(() =>
    {
        DeployWebsite(new DeploySettings()
        {
            SourcePath = publishDir.Path.FullPath,
            SiteName = siteName,
            ComputerName = deployUrl,
            Username = userName,
            Password = deployPassword
        });
    });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task ("Default")
    .IsDependentOn ("Restore")
    .IsDependentOn ("Build");
    //.IsDependentOn ("Publish")
    //.IsDependentOn ("Deploy");

Task ("Rebuild")
    .IsDependentOn("Clean")
    .IsDependentOn ("Restore")
    .IsDependentOn ("Build");

RunTarget (target);