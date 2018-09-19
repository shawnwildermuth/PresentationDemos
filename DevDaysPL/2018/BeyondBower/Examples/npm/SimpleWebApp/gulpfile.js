/// <binding AfterBuild='default' ProjectOpened='default' />
var gulp = require("gulp");
var rimraf = require("rimraf");
var merge = require('merge-stream');

// Dependency Dirs
var deps = {
  "jquery": {
    "dist/*": "dist"
  },
  "bootstrap": {
    "dist/**/*": "dist"
  },
  "jquery-validation": {
    "dist/*": "dist"
  },
  "jquery-validation-unobtrusive": {
    "dist/*": "dist"
  },

};

gulp.task("clean", function (cb) {
  return rimraf("wwwroot/lib/", cb);
});

gulp.task("scripts", function () {

  var streams = [];

  for (var prop in deps) {
    console.log("Prepping Scripts for: " + prop);
    for (var itemProp in deps[prop]) {
      streams.push(gulp.src("node_modules/" + prop + "/" + itemProp)
        .pipe(gulp.dest("wwwroot/lib/" + prop + "/" + deps[prop][itemProp])));
    }
  }

  return merge(streams);

});

gulp.task("default", ['scripts']);
