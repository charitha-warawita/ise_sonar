<template>
 <p>ItemType: {{itemType}} ; TAId: {{ taId }} ; QID: {{ quotaid }} ; showQuotaCondition: {{ showQuotaCondition }}</p>
  <div class="container">
    <div class="row">
      <div class="col-md-6">
        <label for="input" class="form-label">Name</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.name" />
      </div>
      <div class="col-md-6">
        <label for="sel1" class="form-label">Servey Quota Type</label>
        <select v-model="key" @change="useQuotaDataStore.serveyQuotaTyp($event)" class="form-select" id="sel2" name="sellist1">
         <option v-bind:value=ServeyQuotaTypes v-for="ServeyQuotaTypes in useQuotaDataStore.currentQuota.ServeyQuotaType" :key="ServeyQuotaTypes">{{ServeyQuotaTypes}}</option>
         <label for="sel1" class="form-label">Select list (select one):</label>
          <!-- <option>Client</option>
          <option>Control</option>
          <option>Supplier</option> -->
        </select>
      </div>
      <div class="col-md-6">
        <label for="sel2" class="form-label">Adjustment Type</label>
        <select class="form-select" id="sel2" name="sellist2">
          <option>Nominal</option>
          <option>percentage</option>
        </select>
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Status</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.status" />
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Completes</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.completes" />
      </div>
      <div class="col-md-6">
        <label for="input" class="form-label">Prescreens</label>
        <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.prescreens" />
      </div>
       <div class="col-md-6">
        <div class="row">
           <div class="col col-lg-4" style="margin-top: 8%;">
              <label for="input" class="form-label">Field Target</label>
          </div>
          <div class="col col-lg-3">
              <label for="input" class="form-label">Nominal</label>
              <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.fieldTargetNominal" />
          </div>
          <div class="col col-lg-3">
             <label for="input" class="form-label">Percentage(%)</label>
             <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.fieldTargetPercentage" />
          </div>
          
      </div>
      </div>
      <div class="col-md-6">
        <div class="row">
           <div class="col col-lg-4" style="margin-top: 8%;">
              <label for="input" class="form-label">Quota</label>
          </div>
          <div class="col col-lg-3">
              <label for="input" class="form-label">Nominal</label>
              <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.quotaNominal" />
          </div>
          <div class="col col-lg-3">
             <label for="input" class="form-label">Percentage(%)</label>
             <input type="text" class="form-control" v-model="useQuotaDataStore.currentQuota.quotaPercentage" />
          </div>
          
      </div>
      </div>
   
    
      
       <!-- <div class="col-md-6">
        <label for="sel1" class="form-label">Select Condition</label>
        <select
          class="form-select"
          id="sel1"
          name="sellist1"
          v-model="key"
          @change="useQuotaDataStore.selectQuotaCondition($event)">
          <option></option>
        </select>
      </div> -->
      <!-- <div class="col-md-6">
        <label for="sel1" class="form-label">Select Condition</label>
        <select
          class="form-select"
          id="sel1"
          name="sellist1"
          v-model="key"
          @change="useQuotaDataStore.selectQuotaCondition($event)">
          <option v-bind:value=condition selected="[condition === 'None' ? 'selected' : '']"  v-for="condition in useQuotaDataStore.currentQuota.conditions" :key="condition">{{condition}}</option>
        </select>
      </div> -->
    </div>
  </div> 
    <div class="col-md-12 p-4">
      <button class="btn btn-outline-success searchButton" id="addQutobutton" v-on:click="useQuotaDataStore.addCondition">Add condition</button>  
    </div>
 
  <fieldset class="col-md-12 border p-2 fieldsetstyle" v-show ="useQuotaDataStore.conditiongrid">
      <legend  class="float-none w-auto" style="font-size: 18px;">Condition</legend>
      <div class="container">
          <div class="row">
               <div class="col-md-3">
                  <label for="sel1" class="form-label">Select Condition</label>
              </div>
              <div class="col-md-6">
                  <select class="form-select" id="sel1" name="sellist1" v-model="key" @change="useQuotaDataStore.selectQuotaCondition($event)">
                  <option v-bind:value=condition selected="[condition === 'None' ? 'selected' : '']"  v-for="condition in useQuotaDataStore.currentQuota.conditions" :key="condition">{{condition}}</option>
                  
                  <!-- <option v-bind:value=condition selected="[condition === 'None' ? 'selected' : '']"  v-for="condition in useQuotaDataStore.currentQuota.conditions" :key="condition">{{condition}}</option> -->
              </select>
              </div>
          </div>
           <div v-if="useQuotaDataStore.showSubPopup">
  <div class="container conditionbox" v-if="useQuotaDataStore.showQuotaCondition === 'none'">
  </div>
  <div class="container conditionbox" v-if="useQuotaDataStore.showQuotaCondition === 'country'">
    <div class="row">
      <div class="col-md-12">
        <div class="card" style="margin-top: 5%">
          <div div class="card-body">
            <div class="col-md-12"><h5>Select countries</h5></div>
            <div
              style="display: inline-block"
              v-for="item in useQuotaDataStore.quotaCountries"
              :key="item.id"
            >
              <div class="form-check form-check-inline">
                <input
                  class="form-check-input"
                  type="checkbox"
                  v-model="item.selected"
                  :checked="item.selected"
                  id="inlineCheckbox1"
                  :value="item.id"
                />
                <label class="form-check-label" for="inlineCheckbox1">{{
                  item.name
                }}</label>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="container conditionbox" v-if="useQuotaDataStore.showQuotaCondition === 'age'">
    <div class="row">
      <div class="col-md-12">
        <div class="card" style="margin-top: 5%">
          <div div class="card-body">
            <div class="col-md-12">
              <h5>Current Age range is set to {{ currAgeRange }}. Enter new age range</h5>
            </div>
            <div class="row">
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Min Age</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputEmail4"
                  v-model="useQuotaDataStore.currentQuota.quotaMinAge"
                />
              </div>
              <div class="col-md-6">
                <label for="inputEmail4" class="form-label">Max Age</label>
                <input
                  type="text"
                  class="form-control"
                  id="inputEmail4"
                  v-model="useQuotaDataStore.currentQuota.quotaMaxAge"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="container conditionbox" v-if="useQuotaDataStore.showQuotaCondition === 'gender'">
    <div class="row">
      <div class="col-md-12">
        <div class="card" style="margin-top: 5%">
          <div class="row">
            <div div class="card-body">
              <div class="col-md-12"><h5>Select genders</h5></div>
              <div class="col-md-12">
                <div
                  style="display: inline-block"
                  v-for="item in useQuotaDataStore.quotaGenders"
                  :key="item.id"
                >
                  <div class="form-check form-check-inline">
                    <input
                      class="form-check-input"
                      type="checkbox"
                      v-model="item.selected"
                      :checked="item.selected"
                      id="inlineCheckbox1"
                      :value="item.id"
                    />
                    <label class="form-check-label" for="inlineCheckbox1">{{
                      item.name
                    }}</label>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  </div>
      </div>
            
  </fieldset>
  
  <div class="col-md-12 p-4">
    <button
      class="btn btn-outline-success searchButton me-2"
      style="width: 100%; margin: 5px 0"
      v-on:click="
        useQuotaDataStore.SaveQuota(useQuotaDataStore.showQuotaCondition, taId, quotaid)
      "
    >
      Save quota
    </button>
  </div>
</template>
<script setup>
import { useQuotaStore } from "@/stores/quotaStore";
import {useProjectStore} from '@/stores/projectStore';
import { storeToRefs } from "pinia";
import { onMounted } from "vue";
const props = defineProps(["itemType", "taId", "quotaid"])
const { project} = storeToRefs(useProjStore)

var useProjStore = useProjectStore();
var useQuotaDataStore = useQuotaStore();

const {
  name,
  fieldTarget,
  status,
  completes,
  prescreence,
  quotaMinAge,
  quotaMaxAge,
} = storeToRefs(useQuotaDataStore);
</script>

<style>
.fieldsetstyle {
  border: 1px solid #8f959b!important;
  }
</style>