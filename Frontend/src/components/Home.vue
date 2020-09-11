<template>
    <div class="home">
    <h1>Results:</h1>
    <ul>
        <li v-for="vehicle of vehicles" :key="vehicle.id">
            <p>Vehicle ID: {{vehicle.Id}}</p>
            <p>License Plate: {{vehicle.LicensePlate}}</p>
            <p>Vehicle Manufacturer: {{vehicle.VehicleManufacturer}}</p>
            <p>Vehicle Type: {{vehicle.VehicleType}}</p>
            <p>Fuel Type: {{vehicle.FuelType}}</p>
        </li>
    </ul>
    </div>
</template>

<script>
    import axios from 'axios';
    export default {
        name: 'body',
        data() {
            return {
                vehicles: null
            };
        }, mounted() {
            axios
                .get('http://localhost:3816/api/vehicle')
                .then(response => (this.vehicles = response.data));
        }
    };



    //export default {
    //    name: 'Home',
    //    props: {
    //        msg: String
    //    }
    //};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
