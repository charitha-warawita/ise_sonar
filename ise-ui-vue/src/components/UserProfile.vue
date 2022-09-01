<template lang="">
    <div v-if="error" class="notification is-danger is-4 title">
        {{ error }}
    </div>
    <div v-if="user && !error" class="row g-3">
        <div class="col-md-4">
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
              <button class="btn btn-outline-success searchButton me-2" @click="shallowLogout()">
                <span class="icon">
                  <i class="fas fa-sign-out-alt fa-fw" />
                </span>
                <span>Logout (Local)</span>
              </button>
            </div><br>
            <div class="column">
              <button class="btn btn-outline-success searchButton me-2" @click="fullLogout()">
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
        </div>

        <!--<div class="column is-full">
          <Search :user="user" :access-token="accessToken" />
        </div>-->
      </div>
      <CustomModal @close="toggleModal('9990')" :modalId="9990">
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
    </CustomModal> 
</template>
<script setup>
    import {useUserStore} from '@/stores/userStore'
    import {storeToRefs} from 'pinia'
    import CustomModal from '@/components/CustomModal.vue'
    import { useRouter } from 'vue-router';
    var userStore = useUserStore()

    const { user, accessToken, graphDetails, graphPhoto, showUserDetails, showGraphDetails, showTokenDetails, error } = storeToRefs(userStore)
    const router = useRouter();
    const toggleModal = (id) => {
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
    };

    function shallowLogout() {
        userStore.shallowLogout();
        router.push('/');
    }

    async function fullLogout() {
        await userStore.fullLogout();
        // userStore.shallowLogout();
        router.push('/');
    }
</script>
<style lang="">
    
</style>