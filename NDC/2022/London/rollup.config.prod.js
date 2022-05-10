import { terser } from "rollup-plugin-terser";
import config from "./rollup.config";

// Customize the configuration
config.plugins.push(terser());
config.output = {
  file: 'public/js/main.min.js',
  format: 'iife'
};

export default {
  ...config
};