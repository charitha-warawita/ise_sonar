<template>
    <div>
        <ul class="nav nav-pills nav-fill border-bottom">
            <li class="nav-item">
            <a v-on:click="useProjsStore.getProjectsBySearchNameAndStartDate(7)" :class="[currentStatus === 7 ? 'active' : '']" class="nav-link" aria-current="page" href="#">All</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjsStore.getProjectsBySearchNameAndStartDate(2)" :class="[currentStatus === 2 ? 'active' : '']" class="nav-link" href="#">Active</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjsStore.getProjectsBySearchNameAndStartDate(1)" :class="[currentStatus === 1 ? 'active' : '']" class="nav-link" href="#">Created</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjsStore.getProjectsBySearchNameAndStartDate(0)" :class="[currentStatus === 0 ? 'active' : '']" class="nav-link" href="#">Draft</a>
            </li>
            <li class="nav-item">
            <a v-on:click="useProjsStore.getProjectsBySearchNameAndStartDate(5)" :class="[currentStatus === 5 ? 'active' : '']" class="nav-link" href="#">Closed</a>
            </li>
        </ul><br /><br/>
        <div class="d-flex">
            <input class="form-control me-2" type="name" placeholder="Type name here" v-model="searchByName">
            <!--<input class="form-control me-2" type="date" placeholder="Start date  ." aria-label="Date" v-model="searchByStartDate">-->
            <a class="btn btn-outline-success searchButton me-2 " v-on:click="useProjsStore.getProjectsBySearchNameAndStartDate(useProjsStore.currentStatus)">Filter</a>
            <a class="btn btn-outline-success searchButton me-2" v-on:click="useProjsStore.setDefaultProjectList">Clear</a>
        </div>
        <br/><br/>
        <ProjectListTable class="projListTable" :fields='displayfields' :projects='useProjsStore.currentProjects' :fieldTitles='displayFieldFormatted'></ProjectListTable>
        <div v-if="currentProjects" style="width:100%; text-align: center"><vue-awesome-paginate
            :total-items="totalItems"
            :items-per-page="currentPageRowCount"
            :max-pages-shown="5"
            :current-page="currentPageNumber"
            :on-click="onClickHandler"
        /></div>
        <!--<nav aria-label="Page navigation example">
            <ul class="pagination justify-content-center">
                <li class="page-item disabled">
                    <a class="page-link" href="#" tabindex="-1">Previous</a>
                </li>
                <li class="page-item"><a class="page-link" href="#">1</a></li>
                <li class="page-item active"><a class="page-link" href="#">2</a></li>
                <li class="page-item"><a class="page-link" href="#">3</a></li>
                <li class="page-item">
                    <a class="page-link" href="#">Next</a>
                </li>
            </ul>
        </nav>-->
        <p v-if="projectListLoading">Loading project list.. </p>
        <p v-if="projectListError"> {{ error }} </p>
    </div>
</template>
<script setup>
import {useProjectsStore} from '@/stores/projectsStore'
import {storeToRefs} from 'pinia'
import ProjectListTable from '@/components/ProjectListTable.vue'
import ProjectModel from '@/models/projectModel.js'
import {ref} from "vue"
import { onMounted } from 'vue'

import CustomModal from '@/components/CustomModal.vue'
import ProjectSetting from '@/components/ProjectSetting.vue'

// console.log(storeToRefs(useProjectStore()).projects);
var useProjsStore = useProjectsStore()
const { searchByName, searchByStartDate, currentProjects, currentStatus, projectListLoading, projectListError, currentPageRowCount, currentPageNumber, totalItems } = storeToRefs(useProjsStore)
const displayfields = ProjectModel.displayfields
const displayFieldFormatted = ProjectModel.displayFieldFormatted

const onClickHandler = (async (page) => {
    console.log(page);
    useProjsStore.currentPageNumber = page;
    useProjsStore.currentProjects = await useProjsStore.GetProjectsList();
  });

onMounted(() => {
    // console.log('on mounted call');
    useProjsStore.setDefaultProjectList()
    // alert(totalItems);
})
</script>
<style>
input[type="date"]:not(.has-value):before{
  color: gray;
  content: attr(placeholder);
}

.projListTable {
    box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
}

.pagination-container {
    display: flex;
    column-gap: 10px;
  }
  .paginate-buttons {
    height: 40px;
    width: 40px;
    border-radius: 20px;
    cursor: pointer;
    background-color: rgb(242, 242, 242);
    border: 1px solid rgb(217, 217, 217);
    color: black;
  }
  .paginate-buttons:hover {
    background-color: #d8d8d8;
  }
  .active-page {
    background-color: #0D6EFD;
    border: 1px solid #0D6EFD;
    color: white;
  }
  .active-page:hover {
    background-color: #2988c8;
  }
</style>