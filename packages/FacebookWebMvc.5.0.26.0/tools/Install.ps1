<<<<<<< HEAD
param($installPath, $toolsPath, $package, $project)
	$project.Object.References.Add("System.Web.Mvc")
=======
param($installPath, $toolsPath, $package, $project)
	$project.Object.References.Add("System.Web.Mvc")
>>>>>>> 8d6fdda124404f2eea2207d3fc53dc1faa69a80b
    $project.Object.References | Where-Object { $_.Name -eq 'Facebook.Web.Mvc.Contracts' } | ForEach-Object { $_.Remove() }