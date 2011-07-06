<<<<<<< HEAD
param($installPath, $toolsPath, $package, $project)
=======
param($installPath, $toolsPath, $package, $project)
>>>>>>> 8d6fdda124404f2eea2207d3fc53dc1faa69a80b
    $project.Object.References | Where-Object { $_.Name -eq 'Facebook.Contracts' } | ForEach-Object { $_.Remove() }