module.exports = {
  content: ["**/*.razor", "**/*.cshtml", "**/*.html"],
  theme: {
    extend: {
      backgroundImage: () => ({
        'hero': "url('/img/hero.jpeg')",
      }),
      boxShadow: {
        'map': '0 0 10px 2px rgba(0, 0, 0, 0.2)',
      },
      screens: {
        'menu': '450px',
      },
    },
  },
  plugins: [require('@tailwindcss/forms')],
};
