<template>
  <div class="centered">
    <!--<p><img class="graphphoto" src="../assets/logo/kantarlogo.svg" alt="kantar" /></p>-->
    <h1>Intelligent Sample Engine</h1><br/>
    <h5 class="title is-5">
      Please log in with Kantar Office365 Credentials
    </h5>
    <button class="btn btn-outline-success searchButton me-2" @click="doLogin">
      Sign in with Microsoft <img class="ml-4" src="../assets/mssymbol.svg" alt="MS logo">
    </button>
    <!--<p class="mt-4">
      Note. You can login with a 'work &amp; school' or personal Microsoft account
    </p>-->

    <div v-if="error" class="notification is-warning mt-4">
      {{ error }}
    </div>
  </div>
</template>

<script>
import auth from '../services/auth'

export default {

  data: function() {
    return {
      error: ''
    }
  },

  methods: {
    async doLogin() {
      try {
        await auth.login()
        this.$emit('loginComplete')
      } catch (err) {
        this.error = err.toString()
      }
    }
  }
}
</script>

<style scoped>
  .centered {
    text-align: center;
  }
</style>