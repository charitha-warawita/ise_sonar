<template >
    <!--<p>Item type: {{itemType}} ; TAID: {{taId }} ; QID : {{ qualificationId }}</p>-->
    <div v-if="itemType === 'profileVars'" class="container">
        <div class="row">
            <p><b>Choose a global profile category</b></p>
            <p>Profiling can help you target in a more specific and efficient way. Please choose any number of profiling attributes that match your target criteria from the profiling categories below.</p>
            <div class="col-md-12">
                <div id="section1" v-if="profCategories">
                    <button 
                        v-for="profCategory in profCategories" 
                        :key="profCategory" 
                        @click="useQualStore.GetQandAForCategory(profCategory)" 
                        type="button" 
                        :id="'profcat'+profCategory.name" 
                        class="btn btn-outline-success me-2 projSettingTogButton"
                        :class="[profCategory.selected ? 'searchButton' : 'btn-light']"
                        >{{ profCategory.name }} - ({{ profCategory.count }})</button>
                </div>
                <p v-if="profCategoriesLoading">Loading categories.. </p>
                <p v-if="profCategoriesError"> {{ profCategoriesError.message }} </p><br/>
                <h5 v-if="selectedCategory !== ''">Profile Question related to <b>{{ selectedCategory }}</b>:</h5>
                <div id="section2" class="profileSection2" v-if="categoryQAs.length > 0">
                    <div v-for="question in categoryQAs" :key="question.Id">
                        <label class="col-md-12"><b>{{ question.name }}</b></label>
                        <label class="col-md-12">{{ question.text }}</label>
                        <button 
                            v-for="answer in question.variables" 
                            :key="answer.id" 
                            type="button" 
                            :id="'answer'+answer.id" 
                            @click="useQualStore.SaveQAToProject(question, answer.id, answer.name)"
                            class="btn btn-outline-success me-2 projSettingTogButton"
                            :class="[answer.selected ? 'searchButton' : 'btn-light']"
                            >{{ answer.name }}</button><hr/>
                    </div>
                </div>
            </div>
        </div>
        <br>
        <button v-if="categoryQAs.length > 0" class="btn btn-outline-success me-2 searchButton" style="width:100%" 
                @click="SaveQualification()">Save Qualifications</button>
    </div>
</template>
<script setup>
import {useQualificationStore} from '@/stores/qualificationStore'
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'
const props = defineProps([ 'itemType' , 'taId', 'qualificationId' ])
const emit = defineEmits(["close"])
var useQualStore = useQualificationStore()

const { profCategories, profCategoriesLoading, profCategoriesError, categoryQAs, selectedCategory } = storeToRefs(useQualStore)

function SaveQualification() {
    useQualStore.saveQualificationDetailstoProject();
    emit("close");
}

onMounted(() => {
    useQualStore.$reset()
})
</script>
<style>
.projectSetting {
    margin: 5px 0 5px 0;
}

.projSettingTogButton {
    margin:2px 0 2px 0; 
    font-size:0.80em;
}

.profileSection2 {
    max-height: 600px; 
    min-height:350px;
    overflow-y:auto; 
    margin-top:25px;
    padding: 10px;
    box-shadow: rgba(67, 71, 85, 0.27) 0px 0px 0.25em, rgba(90, 125, 188, 0.05) 0px 0.25em 1em;
}
</style>