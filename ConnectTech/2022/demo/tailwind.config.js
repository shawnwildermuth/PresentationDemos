/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./public/**/*.{js,html,css}"
  ],
  //prefix: "tw-",
  theme: {
    // units: {

    // },
    extend: {
      spacing: {
        "92": "23rem"
      }
    },
  },
  plugins: [],
}
