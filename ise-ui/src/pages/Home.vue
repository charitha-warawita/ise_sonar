<script setup lang="ts">
import Panel from 'primevue/panel';
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import ProjectsGrid from "../components/ProjectsGrid.vue";    
import { subDays, format } from 'date-fns';
import { ref } from 'vue';

const projectFilter = ref("something");
const projects = [
    {
        Id: "12345",
        Name: "Something",
        CreationDate: format(subDays(new Date(), 25), "dd/MM/yyyy"),
        LastActivity: format(subDays(new Date(), 5), "dd/MM/yyyy - hh:mm aa"),    
    },
    {
        Id: "12345",
        Name: "Something",
        CreationDate: format(subDays(new Date(), 25), "dd/MM/yyyy"),
        LastActivity: format(subDays(new Date(), 5), "dd/MM/yyyy - hh:mm aa"),    
    },
    {
        Id: "12345",
        Name: "Something",
        CreationDate: format(subDays(new Date(), 25), "dd/MM/yyyy"),
        LastActivity: format(subDays(new Date(), 5), "dd/MM/yyyy - hh:mm aa"),    
    }
]
</script>

<template>
    <div class="home-container" >
        <div class="home-top-row">
            <div style="flex-grow: 1;">
                <span class="p-float-label">
                    <InputText id="search" type="text" :model="projectFilter" />
                    <label for="search">Search</label>
                </span>
                
            </div>
            <div>
                <Button label="Create Project" />
            </div>
        </div>
        <div>
            <Panel :value="projects">
                <template #header>
                    <span>
                        <!-- Needs to be bold -->
                        <b>Projects <span style="color: lightskyblue;">{{ projects.length }}</span></b>
                    </span>
                </template>

                <ProjectsGrid :value="projects" />
            </Panel>            
        </div>
    </div>
</template>

<style scoped>

.home-container {
    display: flex;
    flex-direction: column;
}

.home-top-row {
    display: flex;
    flex-direction: row;
    margin-bottom: 30px;
}

.p-panel :deep(.p-panel-header) {
    border: none;
    box-shadow: 0px 0px 12px lightgrey;
    z-index: 1;
    background-color: var(--color-background);
}

.p-panel :deep(.p-panel-content) {
    border: none;
    box-shadow: 0px 0px 12px lightgrey;        
}

</style>