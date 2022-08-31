<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
      <div class="container-fluid">
        <a class="navbar-brand" href="#"><img src="@/assets/logo.svg" alt="" width="30" height="24"> {{ apptitle }}</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent"  >
          <ul class="navbar-nav me-auto mb-2 mb-lg-0"  >
            <li class="nav-item" v-show="activecontent">
              <RouterLink @click="activate(1)" :class="{ active : active_el == 1 }" class="nav-link" to="/" >Projects</RouterLink>
            </li>
            <li class="nav-item" v-show="activecontent">
              <RouterLink @click="activate(2)" :class="{ active : active_el == 2 }" class="nav-link" to="/create">Create Projects</RouterLink>
            </li>
            <li class="nav-item" v-show="activecontent">
              <RouterLink @click="activate(3)" :class="{ active : active_el == 3 }" class="nav-link" to="/about">About</RouterLink>
            </li>
          </ul>
          <!--<div class="dropdown">
            <button class="btn btn-primary dropdown-toggle btn-outline-success searchButton" type="button" 
                                      id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
              {{this.versiontext}}
            </button>
            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton1">
                <li><RouterLink @click="activate(4)" :class="{ active : active_el == 4 }" class="dropdown-item" to="/">Proof of concept</RouterLink></li>
                <li><RouterLink @click="activate(5)" :class="{ active : active_el == 5 }" class="dropdown-item" to="/about">Version 1</RouterLink></li>
            </ul>
          </div>-->
          
          <div class="dropdown"> 
          <div class="dropdown-toggle" style="color:white" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
              <a href="#" data-toggle="dropdown" style="color:white">
                <img v-if="graphSmallPhoto" :src="graphSmallPhoto" class="avatar" alt="Avatar">
                <img v-if="!graphSmallPhoto" src="/src/assets/user.png" class="avatar" alt="Avatar">  {{ user.name }}  <b class="caret"></b></a>
            </div>
            
            <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="dropdownMenuButton1">
                <li><RouterLink class="dropdown-item" to="/user">Profile</RouterLink></li>
                <li><RouterLink @click="userStore.shallowLogout()" class="dropdown-item" to="/">Logout</RouterLink></li>
            </ul>
          </div>
        </div>
      </div>
    </nav>
</template>
<script setup>
  import {useUserStore} from '@/stores/userStore'
  import {storeToRefs} from 'pinia'
  var userStore = useUserStore()
  const { user, graphSmallPhoto } = storeToRefs(userStore)
  var active_el = 1;
  var activecontent = true;
  var versiontext = 'Proof of Concept';
  var apptitle = import.meta.env.VITE_APP_TITLE;
  var mode = import.meta.env.VITE_ENV;

  function activate(el) {
    this.active_el = el;
        if (this.active_el ===5) {
          this.activecontent = false
          this.versiontext='Version 1'
        } else { (this.active_el ===4)
          this.activecontent = true
            this.versiontext='Proof of concept'
        }
  }

</script>
<!--<script>
export default {
  data() {
    console.log('Test: ' + JSON.stringify(import.meta.env))
    return {
      active_el: 1,
      activecontent : true,
      versiontext:'Proof of concept',
      apptitle: import.meta.env.VITE_APP_TITLE,
      mode: import.meta.env.VITE_ENV
    }
  },
  methods:{
    activate:function(el){
        this.active_el = el;
        if (this.active_el ===5){
          this.activecontent = false
          this.versiontext='Version 1'
        }else{(this.active_el ===4)
          this.activecontent = true
            this.versiontext='Proof of concept'
        }
    }
  }   
  }
</script> -->
<style>
  .avatar {
    border-radius: 50%;
  }
    .searchButton {
        background-color: #34495E;
        color: white;
    }
</style>