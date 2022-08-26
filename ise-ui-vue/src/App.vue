<template>
  <div>
    <div class="container is-fluid" style="margin: 20px 0 20px 0">
      <div v-if="error" class="notification is-danger is-4 title">
        {{ error }}
      </div>

      <Login v-if="!user && !error" @loginComplete="updateUser()" />

      <div v-if="user && !error" class="row g-3">
        <TopHeader />
        <!--<div class="col-md-4">
          <h5>Account &amp; Tokens</h5><hr>
          <p><b>Name:</b> {{ user.name }}</p>
          <p><b>Username:</b> {{ user.username }}</p>
          <br />
          <button class="btn btn-outline-success searchButton me-2" @click="toggleModal(9990); showUserDetails = true">
            <span class="icon">
              <i class="fas fa-user fa-fw" />
            </span>
            <span>ID Token &amp; Account</span>
          </button>
          <button class="btn btn-outline-success searchButton me-2" @click="toggleModal(9992); showTokenDetails = true">
            <span class="icon">
              <i class="fas fa-code fa-fw" />
            </span>
            <span>Access Token</span>
          </button>

          <div class="columns mt-2">
            <div class="column">
              <button class="btn btn-outline-success searchButton me-2" @click="shallowLogout">
                <span class="icon">
                  <i class="fas fa-sign-out-alt fa-fw" />
                </span>
                <span>Logout (Local)</span>
              </button>
            </div><br>
            <div class="column">
              <button class="btn btn-outline-success searchButton me-2" @click="fullLogout">
                <span class="icon">
                  <i class="fas fa-door-open fa-fw" />
                </span>
                <span>Logout (Full)</span>
              </button>
            </div>
          </div>
        </div>
        <div v-if="graphDetails" class="col-md-4">
          <h5>Graph Details</h5><hr>
          <p><b>UPN:</b> {{ graphDetails.userPrincipalName }}</p>
          <p><b>ID:</b> {{ graphDetails.id }}</p>
          <p><b>Job Title:</b> {{ graphDetails.jobTitle }}</p>
          <p><b>Location:</b> {{ graphDetails.officeLocation }}</p>
          <p><b>Mobile:</b> {{ graphDetails.mobilePhone }}</p>
          <p><b>Department:</b> {{ graphDetails.department }}</p>
          <button class="btn btn-outline-success searchButton me-2" @click="toggleModal(9991); showGraphDetails = true">
            <span class="icon">
              <i class="fas fa-address-card fa-fw" />
            </span>
            <span>Full Graph Result</span>
          </button>
        </div>

        <div v-if="graphPhoto" class="col-md-4">
          <h5>Photo</h5><hr>
          <p><img class="graphphoto" :src="graphPhoto" alt="user" /></p>
        </div>-->

        <!--<div class="column is-full">
          <Search :user="user" :access-token="accessToken" />
        </div>-->
        <RouterView />
      </div>
    </div>
    <!--<CustomModal @close="toggleModal('9990')" :modalId="9990">
        <div class="card modal-content">
          <h3 class="card-header">Account &amp; ID Token Details</h3>
            <div class="card-body">
            <pre>{{ JSON.stringify(user, null, 2) }}</pre></div>
          </div>
    </CustomModal>
    <CustomModal @close="toggleModal('9991')" :modalId="9991">
        <div class="card modal-content">
          <h3 class="card-header">Full Graph Details</h3>
            <div class="card-body">
            <pre>{{ JSON.stringify(graphDetails, null, 2) }}</pre></div>
          </div>
    </CustomModal>
    <CustomModal @close="toggleModal('9992')" :modalId="9992">
        <div class="card modal-content">
          <h3 class="card-header">Access Token Raw Value
          <a target="_blank" href="https://jwt.ms">https://jwt.ms</a></h3>
            <div class="card-body">
            <pre>{{ accessToken }}</pre></div>
          </div>
    </CustomModal> -->


    
  </div>
</template>
<script setup>
import {useUserStore} from '@/stores/userStore'
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'
import { RouterLink, RouterView } from 'vue-router'
import TopHeader from '@/components/TopHeader.vue'
import Login from '@/components/Login.vue'

var userStore = useUserStore()
const { user, error } = storeToRefs(userStore)

function updateUser() {
  userStore.updateUser();
  userStore.fetchGraphDetails();
}

onMounted(async () => {
    await userStore.created();
    await userStore.fetchGraphDetails();
})
</script>
<!--<script>
import { RouterLink, RouterView } from 'vue-router'
import TopHeader from '@/components/TopHeader.vue'
import auth from '@/services/auth'
import graph from '@/services/graph'
import Login from '@/components/Login.vue'
import CustomModal from '@/components/CustomModal.vue'

export default {
  components: { TopHeader, Login, CustomModal },

  data: function() {
    return {
      // User account object synced with MSAL getAccount()
      user: {},
      // Access token fetched via MSAL for calling Graph API
      accessToken: '',

      // Details fetched from Graph API, user object and photo
      graphDetails: null,
      graphPhoto: null,

      // Visibility toggles for the three details modal popups
      showUserDetails: false,
      showGraphDetails: false,
      showTokenDetails: false,

      // Any errors
      error: ''
    }
  },

  watch: {
    // If user changes, make calls to Graph API
    user: async function() {
      this.fetchGraphDetails()
    }
  },

  async created() {
    // Basic setup of MSAL helper with client id, or give up
    if (import.meta.env.VITE_APP_CLIENT_ID) {
      auth.configure(import.meta.env.VITE_APP_CLIENT_ID, false)

      // Restore any cached or saved local user
      this.user = auth.user()
      console.log(`configured ${auth.isConfigured()}`)
    } else {
      this.error = 'VITE_APP_CLIENT_ID is not set, the app will not function! 😥'
    }
  },

  methods: {
    // Update user from MSAL
    updateUser() {
      this.user = auth.user()
    },

    // Remove locally held user details, is same as logout
    shallowLogout() {
      this.user = null
      this.graphDetails = null
      this.userDetails = null
      this.graphPhoto = null
      auth.clearLocal()
    },

    // Full logout local & server side
    fullLogout() {
      auth.logout()
    },

    // Get an access token and call graphGetSelf & graphGetPhoto
    async fetchGraphDetails() {
      if (!this.user || this.user.username == 'demo@example.net') {
        return
      }

      try {
        this.graphDetails = await graph.getSelf()
        this.graphPhoto = await graph.getPhoto()
        this.accessToken = graph.getAccessToken()
      } catch (err) {
        this.error = err
      }
    },
    
    toggleModal(id) {
        // useQuotaDataStore.conditiongrid = false;
        console.log('customModal id or qId is:' + id);
        var classname = 'customModal-' + id
        var element = document.getElementsByClassName(classname)
        if(element[0].style.display === 'none') {
            element[0].style.removeProperty("display")
            element[1].style.removeProperty("display")
        }
        else { 
            element[0].style.display = 'none'
            element[1].style.display = 'none'
        }
    }
  }
}
</script>-->

<style>
@import '@/assets/base.css';

#app {
  max-width: 1280px;
  margin: 0 auto;
  padding: 2rem;
  font-weight: normal;
}

header {
  line-height: 1.5;
  max-height: 100vh;
}

.logo {
  display: block;
  margin: 0 auto 2rem;
}

a,
.green {
  text-decoration: none;
  color: hsla(160, 100%, 37%, 1);
  transition: 0.4s;
}

@media (hover: hover) {
  a:hover {
    background-color: hsla(160, 100%, 37%, 0.2);
  }
}

.custom-toggler .navbar-toggler-icon {
  background-image: url("data:image/svg+xml;charset=utf8,%3Csvg viewBox='0 0 32 32' xmlns='http://www.w3.org/2000/svg'%3E%3Cpath stroke='rgba(255,102,203, 0.5)' stroke-width='2' stroke-linecap='round' stroke-miterlimit='10' d='M4 8h24M4 16h24M4 24h24'/%3E%3C/svg%3E");
}

pre {
 white-space: pre-wrap;       /* css-3 */
 white-space: -moz-pre-wrap;  /* Mozilla, since 1999 */
 white-space: -pre-wrap;      /* Opera 4-6 */
 white-space: -o-pre-wrap;    /* Opera 7 */
 word-wrap: break-word;       /* Internet Explorer 5.5+ */
}
.custom-toggler.navbar-toggler {
  border-color: rgb(255,102,203);
} 

/*nav {
  width: 100%;
  font-size: 12px;
  text-align: center;
  margin-top: 2rem;
}

nav a.router-link-exact-active {
  color: var(--color-text);
}

nav a.router-link-exact-active:hover {
  background-color: transparent;
}

nav a {
  display: inline-block;
  padding: 0 1rem;
  border-left: 1px solid var(--color-border);
}

nav a:first-of-type {
  border: 0;
}*/

@media (min-width: 1024px) {
  body {
    /*display: flex;
    place-items: center;*/
  }

  #app {
    /*display: grid; 
    grid-template-columns: 1fr 1fr; */
    padding: 0 2rem;
  }

  header {
    display: flex;
    place-items: center;
    padding-right: calc(var(--section-gap) / 2);
  }

  header .wrapper {
    display: flex;
    place-items: flex-start;
    flex-wrap: wrap;
  }

  .logo {
    margin: 0 2rem 0 0;
  }

  nav {
    text-align: left;
    font-size: 1rem;

    padding: 1rem 0;
    margin-top: 1rem;
  }
}
</style>
