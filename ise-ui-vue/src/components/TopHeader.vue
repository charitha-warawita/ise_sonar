<template>
    <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
      <div class="container-fluid">
        <a class="navbar-brand" href="#"><img src="@/assets/logo.svg" alt="" width="30" height="24"> ISE</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent" >
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <RouterLink @click="activate(1)" :class="{ active : active_el == 1 }" class="nav-link" to="/" >Projects</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink @click="activate(2)" :class="{ active : active_el == 2 }" class="nav-link" to="/create">Create Projects</RouterLink>
            </li>
            <li class="nav-item">
              <RouterLink @click="activate(3)" :class="{ active : active_el == 3 }" class="nav-link" to="/about">About</RouterLink>
            </li>
           
            
          </ul>
          <!-- <form class="d-flex" role="search">
            <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search">
            <button class="btn btn-outline-success searchButton" type="submit">Search</button>
          </form> -->
             <ul class="navbar-nav">
               <li class="nav-item dropdown">
               <a class="nav-link dropdown-toggle"  href="" id="navbarDarkDropdownMenuLink" role="button" data-bs-toggle="dropdown" aria-expanded="false">
               Version 
             </a>
              <ul class="dropdown-menu dropdown-menu-dark" aria-labelledby="navbarDarkDropdownMenuLink">
               <li><a class="dropdown-item"  href="/">POC</a></li>
                <li><a class="dropdown-item" href="/about">Version 1</a></li>
              </ul>
             </li>
            </ul>
        </div>
      </div>
    </nav>
</template>
<script>
export default {
  data() {
    return {
      active_el: 1
    }
  },
  methods:{
    activate:function(el){
        this.active_el = el;
    }
  } ,
  deactivated:function(el){
    console.log(" call in deactivate")
    this.active_el=el;
  }
    
  }
   

</script>
<style>
    .searchButton {
        background-color: rgb(25, 9, 250);
        color: white;
    }

.navbar-dark .navbar-nav .nav-link {
    color: white;
}

</style>