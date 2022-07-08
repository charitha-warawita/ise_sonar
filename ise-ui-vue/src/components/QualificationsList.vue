<template >
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
                <div style="display: inline-block" v-for="item in useQualStore.countries" :key="item.id">
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="checkbox" v-model=item.selected :checked=item.selected id="inlineCheckbox1" :value=item.id>
                            <label class="form-check-label" for="inlineCheckbox1">{{item.name}}</label>
                        </div>
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
    <div v-if="itemType === 'field of expertise'" class="container">
        <div class="row">
            <div class="col-md-12"><h5>Select Field of Experties</h5></div>
            <div class="col-md-12">
                <div style="display: inline-block" v-for="item in useQualStore.fieldofexperties" :key="item.id">
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="checkbox" v-model=item.selected :checked=item.selected id="inlineCheckbox1" :value=item.id>
                            <label class="form-check-label" for="inlineCheckbox1">{{item.name}}</label>
                        </div>
                </div>
            </div>
            <div class="col-md-12">
                <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useQualStore.SaveFieldofexpertiseQualification(itemType, taId, qualificationId);useQualStore.EditQualificationback()">Save qualification</button>
            </div>
        </div>
    </div>
    <div v-if="itemType === 'online banking'" class="container">
        <div class="row">
            <div class="col-md-12"><h5>Select Online Banking Details</h5></div>
            <div class="col-md-12">
                <div style="display: inline-block" v-for="item in useQualStore.onlinebanking" :key="item.id">
                        <div class="form-check form-check-inline">
                            <input class="form-check-input" type="checkbox" v-model=item.selected :checked=item.selected id="inlineCheckbox1" :value=item.id>
                            <label class="form-check-label" for="inlineCheckbox1">{{item.name}}</label>
                        </div>
                </div>
            </div>
            <div class="col-md-12">
                <button class="btn btn-outline-success searchButton me-2" style="width:100%; margin: 5px 0;" v-on:click="useQualStore.SaveOnlineBankingQualification(itemType, taId, qualificationId);useQualStore.EditQualificationback()">Save qualification</button>
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

const { currAgeRange, minAge, maxAge } = storeToRefs(useQualStore)

/*onMounted(() => {
    // console.log('on mounted call');
    useQualStore.GetQualification(18, 60)
})*/
    
</script>
<style>
    
</style>