#--------------------------------------
# Dependencies
#--------------------------------------
require 'albacore'
#--------------------------------------
# Debug
#--------------------------------------
ENV.each {|key, value| puts "#{key} = #{value}" }
#--------------------------------------
# My environment vars
#--------------------------------------
@env_projectname = ENV['env_projectname']
@env_buildconfigname = ENV['env_buildconfigname']
@env_buildversion = ENV['env_buildversion']
@env_projectfullname = ENV['env_projectfullname']
@env_buildfolderpath = ENV['env_buildfolderpath']
@env_unitTestXmlResultsPath = ENV['env_unitTestXmlResultsPath']
@env_solutionfolderpath = "../"
#--------------------------------------
# Albacore flow controlling tasks
#--------------------------------------
desc "Fixes version, compiles the solution, executes tests and TODO:deploys."
#task :default => [:buildIt, :testIt]
task :default => [:buildIt]

desc "Fixes version and compiles."
task :buildIt => [:versionIt, :compileIt, :copyBinaries]
#task :buildIt => [:versionIt, :compileIt]

desc "Executes all tests."
#task :testIt => [:runUnitTests]

#--------------------------------------
# Albacore tasks
#--------------------------------------
desc "Updates version info."
assemblyinfo :versionIt do |asm|
  sharedAssemblyInfoPath = "#{@env_solutionfolderpath}SharedAssemblyInfo.cs"
  
  asm.input_file = sharedAssemblyInfoPath
  asm.output_file = sharedAssemblyInfoPath
  asm.version = @env_buildversion
  asm.file_version = @env_buildversion  
end

desc "Creates clean build folder structure."
task :createCleanBuildFolder do
  FileUtils.rm_rf(@env_buildfolderpath)
  FileUtils.mkdir_p("#{@env_buildfolderpath}Binaries")
end

desc "Clean and build the solution."
msbuild :compileIt => [:createCleanBuildFolder] do |msb|
  msb.properties :configuration => @env_buildconfigname
  msb.targets :Clean, :Build
  msb.solution = "#{@env_solutionfolderpath}#{@env_projectname}.sln"
end

desc "Copy binaries to output."
task :copyBinaries do
	excludedDirectories = "obj"
	logPath = "Robocopy.log"

    robocopy = "robocopy " \
               "\"#{@env_solutionfolderpath}\" " \
               "\"#{@env_buildfolderpath}\" " \
               "/MIR " \
               "/XD #{excludedDirectories} " \
               "/LOG+:\"#{logPath}\" " \
               "/TEE"

	 sh robocopy do |ok,res|
                     raise "Robocopy failed with exit " \
                           "code #{res.exitstatus}." \
                     if res.exitstatus > 8
                end
end

desc "Run unit tests."
nunit :runUnitTests do |nunit|
  nunit.command = "#{@env_solutionfolderpath}packages/NUnit.2.5.10.11092/tools/nunit-console.exe"
  nunit.options "/framework=v4.0.30319","/xml=#{@env_unitTestXmlResultsPath}"
  nunit.assemblies = FileList["#{@env_solutionfolderpath}*Test*/bin/#{@env_buildconfigname}/*.Test*.dll"].exclude(/obj\//)
end