<template>
    <RouterLink :to="routerLinkTo" class="btn btn-outline-primary" >{{title}}</RouterLink>
</template>
<script>
    export default{
        props:['title', 'routerLinkTo']
    }
</script>