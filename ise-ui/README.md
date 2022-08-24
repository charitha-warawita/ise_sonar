# ise-ui

## Recommended IDE Setup

[VSCode](https://code.visualstudio.com/) + [Volar](https://marketplace.visualstudio.com/items?itemName=Vue.volar) (and disable Vetur)

## Type Support for `.vue` Imports in TS

1. Disable the built-in TypeScript Extension
   1. Run `Extensions: Show Built-in Extensions` from VSCode's command palette
   2. Find `TypeScript and JavaScript Language Features`, right click and select `Disable (Workspace)`
2. Reload the VSCode window by running `Developer: Reload Window` from the command palette.

## Customize configuration

See [Vite Configuration Reference](https://vitejs.dev/config/).

## Project Setup

```sh
npm install
```

### Compile and Hot-Reload for Development

```sh
npm run dev
```

### Type-Check, Compile and Minify for Production

```sh
npm run build
```

### Run Unit Tests with [Vitest](https://vitest.dev/)

```sh
npm run test:unit
```

### Run End-to-End Tests with [Cypress](https://www.cypress.io/)

```sh
npm run build
npm run test:e2e # or `npm run test:e2e:ci` for headless testing
```

### Lint with [ESLint](https://eslint.org/)

```sh
npm run lint
```

## Tech Stack

- [Vue 3](https://vuejs.org/guide/introduction.html)
- [Pinia](https://pinia.vuejs.org/introduction.html)
- [TypeScript](https://www.typescriptlang.org/docs/handbook/)
- [SCSS](https://sass-lang.com/documentation/)
- [PrimeVue](https://www.primefaces.org/primevue/) (Dependent on [PrimeIcons](https://www.primefaces.org/primevue/icons) and [PrimeFlex](https://www.primefaces.org/primeflex/))
- [vuelidate](https://vuelidate-next.netlify.app/)
- [date-fns](https://date-fns.org/)
- [zod](https://zod.dev/)
- [eslint](https://eslint.org/docs/latest/user-guide/getting-started)
- [prettier](https://prettier.io/docs/en/index.html)
