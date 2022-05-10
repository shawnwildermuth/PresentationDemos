import resolve from "@rollup/plugin-node-resolve";
import json from "rollup-plugin-json";
import commonjs from "@rollup/plugin-commonjs";

export default {
  input: "src/main.js",

  output: {
    file: "public/js/main.js",
    format: "es",
    sourcemap: true,
  },


  plugins: [
    resolve({ module: true, jsnext: true, main: true, browser: true }),
    json(),
    commonjs()
  ],

  watch: {
    buildDelay: 1000,
  },
};
