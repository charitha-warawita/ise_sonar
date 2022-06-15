<template>
    <div class="pageTitle">
        <h4>{{ title }}</h4>        
    </div>
</template>
<script>
export default {
    props: {
        title: String

    } 
}
</script>
<style>
.pageTitle {
    margin-top: 20px;
    margin-bottom: 20px;
}
    
</style>