<<<<<<< HEAD
param($installPath, $toolsPath, $package, $project)
	$project.Object.References.Add("System.Web")
=======
param($installPath, $toolsPath, $package, $project)
	$project.Object.References.Add("System.Web")
>>>>>>> 8d6fdda124404f2eea2207d3fc53dc1faa69a80b
    $project.Object.References | Where-Object { $_.Name -eq 'Facebook.Web.Contracts' } | ForEach-Object { $_.Remove() }