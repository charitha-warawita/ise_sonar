<template >
    <!--<p>Item type: {{itemType}} ; TAID: {{taId }} ; QID : {{ qualificationId }}</p>-->
    <div v-if="itemType === 'age'" class="container">
        <div class="row">
            <div class="col-md-12"><h5>Current Age range is set to {{ currAgeRange}}. Enter new age range</h5></div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Min Age</label>
                <input type="text" class="form-control" id="inputEmail4" v-model=minAge >
            </div>
            <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Max Age</label>
                <input type="text" class="form-control" id="inputEmail4" v-model=maxAge>
            </div>
            <div class="col-md-12">
                <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useQualStore.SaveAgeQualification(itemType, taId, qualificationId)">Save qualification</button>
            </div>
        </div>
    </div>
    <div v-if="itemType === 'country'" class="container">
        <div class="row">
            <div class="col-md-12"><h5>Select countries</h5></div>
            <div class="col-md-12">
                <p v-if="countriesLoading">Loading categories.. </p>
                <p v-if="countriesError"> {{ countriesError.message }} </p>
                <div v-if="countries">
                    <button 
                        v-for="country in countries" 
                        :key="country.id" 
                        @click="useQualStore.UpdateCountrySelection(itemType, taId, qualificationId, country)" 
                        type="button" 
                        :id="'country'+country.id" 
                        class="btn btn-outline-success me-2 projSettingTogButton"
                        :class="[country.selected ? 'searchButton' : 'btn-light']"
                        >{{ country.name }}</button>
                </div>
            </div>
            <div class="col-md-12">
                <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useQualStore.SaveCountryQualification(itemType, taId, qualificationId)">Save qualification</button>
            </div>
        </div>
    </div>
    <div v-if="itemType === 'gender'" class="container">
        <div class="row">
            <div class="col-md-12"><h5>Select genders</h5></div>
            <div class="col-md-12">
                <div style="display: inline-block" v-for="item in useQualStore.genders" :key="item.id">
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="checkbox" v-model=item.selected :checked=item.selected id="inlineCheckbox1" :value=item.id>
                            <label class="form-check-label" for="inlineCheckbox1">{{item.name}}</label>
                        </div>
                </div>
            </div>
            <div class="col-md-12">
                <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useQualStore.SaveGenderQualification(itemType, taId, qualificationId)">Save qualification</button>
            </div>
        </div>
    </div>
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
    </div>
</template>
<script setup>
import {useQualificationStore} from '@/stores/qualificationStore'
import {storeToRefs} from 'pinia'
import { onMounted } from 'vue'
const props = defineProps([ 'itemType' , 'taId', 'qualificationId' ])

var useQualStore = useQualificationStore()

const { currAgeRange, minAge, maxAge, countries, countriesLoading, countriesError,
profCategories, profCategoriesLoading, profCategoriesError, categoryQAs, selectedCategory } = storeToRefs(useQualStore)
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