<template>
    <div>
        <ul class="nav nav-pills nav-fill border-bottom">
            <li class="nav-item">
            <a v-on:click="activetab='All'; filterStatus('All')" :class="[activetab === 'All' ? 'active' : '']" class="nav-link" aria-current="page" href="#">All</a>
            </li>
            <li class="nav-item">
            <a v-on:click="activetab='Active'; filterStatus('Active')" :class="[activetab === 'Active' ? 'active' : '']" class="nav-link" href="#">Active</a>
            </li>
            <li class="nav-item">
            <a v-on:click="activetab='Draft'; filterStatus('Draft')" :class="[activetab === 'Draft' ? 'active' : '']" class="nav-link" href="#">Draft</a>
            </li>
            <li class="nav-item">
            <a v-on:click="activetab='Closed'; filterStatus('Closed')" :class="[activetab === 'Closed' ? 'active' : '']" class="nav-link" href="#">Closed</a>
            </li>
        </ul><br /><br/>
        <div class="d-flex">
            <input class="form-control me-2" type="name" placeholder="Type name here" v-model="sName">
            <input class="form-control me-2" type="date" placeholder="Start date  ." aria-label="Date" v-model="sDate">
            <a class="btn btn-outline-success searchButton me-2 " v-on:click="filterByNameStartDate('test')">Filter</a>
            <a class="btn btn-outline-success searchButton me-2" v-on:click="clearFilters('test')">Clear</a>
        </div>
        <br/><br/>

        <ProjectListTable class="projListTable" :fields='displayfields' :projects='currentProject' :fieldTitles='displayFieldFormatted'></ProjectListTable>
        <ProjectListTable style="display:none" class="projListTable" :fields='displayfields' :projects='tempProject' :fieldTitles='displayFieldFormatted'></ProjectListTable>
        
 
    </div>
</template>
<script setup>
import {useProjectStore} from '@/stores/projectStore'
import {storeToRefs} from 'pinia'
import ProjectListTable from '@/components/ProjectListTable.vue'
import ProjectModel from '@/models/projectModel.js'
import {ref} from "vue"

// console.log(storeToRefs(useProjectStore()).projects);
var useProjStore = useProjectStore()
const { projects, getDraftProjects, getActiveProjects, getClosedProjects } = storeToRefs(useProjStore)
const displayfields = ProjectModel.displayfields
const displayFieldFormatted = ProjectModel.displayFieldFormatted

const activetab = ref('All')
let currentProject = projects.value
const searchName = ref('')
const searchStartDate = ref('')

function filterStatus(status) {
    console.log("filter by status start");
    if(this !== undefined) 
        this.currentProject = useProjectStore().getProjectByStatus(status);
    else
        currentProject = useProjectStore().getProjectByStatus(status);
        
        console.log("filter by status end");
}
</script>
<script>
export default {
    name: "projectList",
    /*props: {
        sName: {
            type: String,
            default: ''
        },
        sDate: {
            type: String,
            default: ''
        }
    },*/
    data() {
        console.log('cp => ' + JSON.stringify(this.cp));
        return {
            sName: '',
            sDate: '',
            tempProject: this.projects
        }
    },
    methods: {
        filterByNameStartDate(test) {
            console.log("filter start");
            var test = this.useProjStore.getProjectByNameStartDate(this.sName, this.sDate);
            this.currentProject = test;
            this.tempProject = test;
            console.log("filter end - " + this.currentProject);
        },
        clearFilters(test) {
            console.log("clear start");
            this.sName = '';
            this.sDate = '';
            var test = this.useProjStore.getProjectByNameStartDate('','');
            this.currentProject = test;
            this.tempProject = test;
            console.log("clear end - " + this.currentProject);
        }
    }
}
</script>
<style>
input[type="date"]:not(.has-value):before{
  color: gray;
  content: attr(placeholder);
}

.projListTable {
    box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
}
</style>