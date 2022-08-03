<template>
  <div class="container conditionscroll">
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
        </select>
      </div>
      <div class="col-md-6">
        <label for="sel2" class="form-label">Adjustment Type</label>
        <select v-model="key" @change="useQuotaDataStore.adjustmentType($event)" class="form-select" id="sel2" name="sellist2">
        <option v-bind:value=adjustmentType v-for="adjustmentType in useQuotaDataStore.currentQuota.adjustmentType" :key="adjustmentType">{{adjustmentType}}</option>

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
                  <option v-bind:value=condition.id v-for="condition in useQuotaDataStore.conditionlist" :key="condition.id">{{condition.categoryName}}-{{condition.name}}</option>
              </select>
              </div>
          </div>
          </div>
           <div class="card" style="margin-top: 5%;border: 1px solid #8f959b!important;" v-show="useQuotaDataStore.selecteDiv">
              <div div class="card-body">
                <div class="subDivQ row g-3">
                  <div class="col-md-9">
                  <label style="display:none">{{useQuotaDataStore.selecttedQuestionid}}</label>
                      <div class="col-md-12"><b>{{useQuotaDataStore.selectedConditioncategoryName}}  {{useQuotaDataStore.selectedConditionName}}</b></div>
                          <div class="col-md-12">{{useQuotaDataStore.selectedConditionText}}</div>
                            <div class="col-md-12">
                                <div style="display: inline-block">
                                    <div class="form-check">
                                      <button 
                                        v-for="answer in useQuotaDataStore.Selectedvariables" 
                                        :key="answer.id" 
                                        type="button" 
                                        :id="'answer'+answer.id" 
                                        @click="useQuotaDataStore.SaveQuotaConditions(useQuotaDataStore.selecttedQuestionid,useQuotaDataStore.selectedConditioncategoryName,useQuotaDataStore.selectedConditionName,useQuotaDataStore.selectedConditionText, answer.id, answer.name)" 
                                        class="btn btn-outline-success me-2 projSettingTogButton"
                                        :class="[answer.selected ? 'searchButton' : 'btn-light']">
                                        {{ answer.name }}</button>
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
import { storeToRefs } from "pinia";
import { onMounted } from "vue";
const props = defineProps(["itemType", "taId", "quotaid"]);

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
.quotaModel{
 max-height: 600px; 
    min-height:350px;
    overflow-y:auto; 
}
</style>