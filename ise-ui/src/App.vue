<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'
import Menu from '@/components/Menu.vue'

</script>

<template>
  <header>
    <Menu></Menu>
  </header>

  <main>
    <RouterView />
  </main>
</template>

<style scoped>
/* Needs media queries */
main {
  flex-grow: 1;
  margin: 30px;
}
</style>
