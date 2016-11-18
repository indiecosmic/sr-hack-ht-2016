(function() {
    "use strict";

    var paths = {
        scripts: {
            src: [
                "Scripts/jquery-3.1.1.js",
                "Scripts/foundation.js",
                "Scripts/app.js"
            ],
            dest: "Content/js/"
        },
        styles: {
            src: [
                "Styles/app.scss"
            ],
            dest: "Content/css/"
        }
    };

    var options = {
        styles: {
            includePaths: [
                "Styles/foundation/"
            ]
        },
        autoprefixer: {
            browsers: [
                "last 3 version",
                "safari 5",
                "ie 8",
                "ie 9"
            ]
        }
    };

    var gulp = require("gulp"),
        autoprefixer = require("gulp-autoprefixer"),
        concat = require("gulp-concat"),
        rename = require("gulp-rename"),
        sass = require("gulp-sass"),
        uglify = require("gulp-uglify");

    function scripts() {
        return gulp.src(paths.scripts.src)
            // expanded output
            .pipe(concat("app.js"))
            .pipe(gulp.dest(paths.scripts.dest))
            // compressed output
            .pipe(rename({
                suffix: ".min"
            }))
            .pipe(uglify().on("error", console.log))
            .pipe(gulp.dest(paths.scripts.dest));
    }

    function styles() {
        return gulp.src(paths.styles.src)
            // expanded output
            .pipe(sass({
                    includePaths: options.styles.includePaths,
                    outputStyle: "expanded"
                })
                .on("error", sass.logError))
            .pipe(autoprefixer(options.autoprefixer))
            .pipe(gulp.dest(paths.styles.dest))
            // compressed output
            .pipe(rename({
                suffix: ".min"
            }))
            .pipe(sass({
                    includePaths: options.styles.includePaths,
                    outputStyle: "compressed"
                })
                .on("error", sass.logError))
            .pipe(autoprefixer(options.autoprefixer))
            .pipe(gulp.dest(paths.styles.dest));
    }

    gulp.task("scripts", scripts);
    gulp.task("styles", styles);

    gulp.task("watch",
        function() {
            gulp.watch(paths.scripts.src, ["scripts"]);
            gulp.watch(paths.styles.src, ["styles"]);
        });

    gulp.task("build", ["scripts", "styles"]);
    gulp.task("default", ["watch"]);
})();