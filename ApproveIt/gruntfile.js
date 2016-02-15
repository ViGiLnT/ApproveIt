/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {

    // Load the package JSON file
    var pkg = grunt.file.readJSON('package.json');

    // get the root path of the project
    var projectRoot = 'C:/Users/ads/Source/Repos/ApproveIt/ApproveIt/';

    var version = "1.0.0";

    grunt.initConfig({
        pkg: pkg,
        clean: {
            files: [
                'files/*.*'
            ]
        },
        copy: {
            release: {
                files: [
                    {
                        expand: true,
                        cwd: projectRoot + 'bin/',
                        src: [
                            pkg.name + '.dll',
                            pkg.name + '.xml'
                        ],
                        dest: 'files/bin/'
                    }
                ]
            }
        },
        umbracoPackage: {
            release: {
                src: 'files/',
                dest: 'bin/umbraco',
                options: {
                    name: pkg.name,
                    version: version,
                    url: pkg.url,
                    license: pkg.license.name,
                    licenseUrl: pkg.license.url,
                    author: pkg.author.name,
                    authorUrl: pkg.author.url,
                    readme: pkg.readme,
                    outputName: pkg.name + '.v' + version + '.zip'
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-umbraco-package');
};