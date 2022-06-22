<template>
    <PageTitle title="Create new project"></PageTitle>
    <CreateProject />  
</template>
<script setup>
import PageTitle from '@/components/PageTitle.vue'
import CreateProject from '@/components/CreateProject.vue'
</script>
<style>
.hidden {
    display: none;
}
.subDiv {
    margin: 20px auto;
    box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;
    padding-bottom: 20px;
    background-color: #E8E8E8;
}

.subDivQ {
    margin: 10px auto;
    background-color: white;
    padding-bottom: 20px;
}

.customItem {
    margin-bottom: 10px;
    box-shadow: rgba(100, 100, 111, 0.2) 0px 7px 29px 0px;
}

input[type=checkbox] {
    transform: scale(1.5);
}

.breakDiv {
    margin:10px 0;
}
    
</style>