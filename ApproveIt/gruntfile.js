/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {

    // Load the package JSON file
    var pkg = grunt.file.readJSON('package.json');

    // get the root path of the project
    var projectRoot = 'C:/Users/ads/Source/Repos/ApproveIt/ApproveIt/';
    var packageNamespace = "Create.Plugin";

    grunt.initConfig({
        pkg: pkg,
        clean: {
            files: [
                'bld/App_Plugins',
                'bld/bin',
                'bld/Umbraco'
            ]
        },
        copy: {
            release: {
                files: [
                    {
                        expand: true,
                        cwd: projectRoot + 'bin/',
                        src: [
                            packageNamespace + '.' + pkg.name + '.dll',
                            'PackageActionsContrib.dll'
                        ],
                        dest: 'bld/bin/'
                    },
                    {
                        expand: true,
                        cwd: projectRoot + 'App_Plugins/',
                        src: ['**'],
                        dest: 'bld/App_Plugins/'
                    },
                    {
                        expand: true,
                        cwd: projectRoot + 'Dashboard/Views/dashboard/approveIt/',
                        src: ['approveItdashboardintro.html'],
                        dest: "bld/Umbraco/Views/dashboard/approveIt/"
                    }
                ]
            }
        },
        umbracoPackage: {
            release: {
                src: 'bld/',
                dest: 'bin/umbraco',
                options: {
                    name: pkg.name,
                    version: pkg.version,
                    url: pkg.url,
                    license: pkg.license.name,
                    licenseUrl: pkg.license.url,
                    author: pkg.author.name,
                    authorUrl: pkg.author.url,
                    readme: pkg.readme,
                    outputName: pkg.name + '.v' + pkg.version + '.zip',
                    manifest: 'package.xml'
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-umbraco-package');
};