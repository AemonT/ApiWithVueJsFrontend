<template>
    <div class="home">

        <form id="saveVehicleForm" method="post">
            <h3>Create new vehicle</h3>
            <p>
                <label for="vehicleId">Vehicle ID:</label>
                <input type="text" name="Id" v-model="Id"  />
            </p>
            <p>
                <label for="licensePlate">License Plate:</label>
                <input type="text" name="LicensePlate" v-model="LicensePlate" />
            </p>
            <p>
                <label for="vehicleManufacturer">Manufacturer:</label>
                <input type="text" name="VehicleManufacturer" v-model="VehicleManufacturer" />
            </p>
            <p>
                <label for="vehicleType">Vehicle Type:</label>
                <input type="text" name="VehicleType" v-model="VehicleType" />
            </p>
            <p>
                <label for="fuelType">Type of Fuel:</label>
                <select name="FuelType" id="FuelType" v-model="FuelType">
                    <option value="Benzine">Benzine</option>
                    <option value="Diesel">Diesel</option>
                    <option value="Electricity">Electricity</option>
                </select>
            </p>
            <input type="button" id="saveVehicle" value="save" v-on:click="sendToServer" />
            <input type="button" id="deleteVehicle" value="delete" v-on:click="deleteFromDatabase" />
            <!--<select name="FuelTypeFilter" id="FuelTypeFilter" v-model="FuelTypeFilter">
                <option value="Benzine">Benzine</option>
                <option value="Diesel">Diesel</option>
                <option value="Electricity">Electricity</option>
            </select>
            <input type="button" id="searchVehicle" value="search" v-on:click="findMyVehicles" />-->
        </form>
        <table class="greenTable">
            <thead>
                <tr>
                    <th>Id</th>
                    <th>License Plate</th>
                    <th>Manufacturer</th>
                    <th>Type</th>
                    <th>Fueltype</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="vehicle of vehicles" :key="vehicle.id">
                    <td>{{vehicle.Id}}</td>
                    <td>{{vehicle.LicensePlate}}</td>
                    <td>{{vehicle.VehicleManufacturer}}</td>
                    <td>{{vehicle.VehicleType}}</td>
                    <td>{{vehicle.FuelType}}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
    import axios from 'axios';
    const connectionUrl = 'http://localhost:3816/api/vehicle';
    export default {
        name: 'body',
        
        data() {
            return {

                Id: "",
                LicensePlate: "",
                VehicleManufacturer: "",
                VehicleType: "",
                FuelType: "",
                FuelTypeFilter: "",
                vehicles: null
            }

        },
        methods: {
            sendToServer: function () {

                axios({
                    method: 'post',
                    url: connectionUrl,
                    data: {
                        "Id": this.Id,
                        "LicensePlate": this.LicensePlate,
                        "VehicleManufacturer": this.VehicleManufacturer,
                        "VehicleType": this.VehicleType,
                        "FuelType": this.FuelType
                    }

                })
                    .then(response => (this.vehicles = response.data))

            },
            deleteFromDatabase: function () {
                axios({
                    method: 'delete',
                    url: connectionUrl,
                    data: {
                        "Id": this.Id
                    }
                }).then(response => (this.vehicles = response.data));
            },
            //TODO: Fix this.
            findMyVehicles: function () {
                axios({
                    method: 'get',
                    url: connectionUrl,
                    data: {
                        "FuelType": this.FuelType
                    }
                }).then(response => (this.vehicles = response.data));
            }

        }
        , mounted() {
            axios
                .get(connectionUrl)
                .then(response => (this.vehicles = response.data));
        }
    }
    

</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    table.greenTable {
        font-family: Georgia, serif;
        border: 6px solid #24943A;
        background-color: #D4EED1;
        width: 100%;
        text-align: center;
    }

        table.greenTable td, table.greenTable th {
            border: 1px solid #24943A;
            padding: 3px 2px;
        }

        table.greenTable tbody td {
            font-size: 13px;
        }

        table.greenTable thead {
            background: #24943A;
            background: -moz-linear-gradient(top, #5baf6b 0%, #3a9e4d 66%, #24943A 100%);
            background: -webkit-linear-gradient(top, #5baf6b 0%, #3a9e4d 66%, #24943A 100%);
            background: linear-gradient(to bottom, #5baf6b 0%, #3a9e4d 66%, #24943A 100%);
            border-bottom: 0px solid #444444;
        }

            table.greenTable thead th {
                font-size: 19px;
                font-weight: bold;
                color: #F0F0F0;
                text-align: left;
                border-left: 2px solid #24943A;
            }

                table.greenTable thead th:first-child {
                    border-left: none;
                }

        table.greenTable tfoot {
            font-size: 13px;
            font-weight: bold;
            color: #F0F0F0;
            background: #24943A;
            background: -moz-linear-gradient(top, #5baf6b 0%, #3a9e4d 66%, #24943A 100%);
            background: -webkit-linear-gradient(top, #5baf6b 0%, #3a9e4d 66%, #24943A 100%);
            background: linear-gradient(to bottom, #5baf6b 0%, #3a9e4d 66%, #24943A 100%);
            border-top: 1px solid #24943A;
        }

            table.greenTable tfoot td {
                font-size: 13px;
            }

            table.greenTable tfoot .links {
                text-align: right;
            }

                table.greenTable tfoot .links a {
                    display: inline-block;
                    background: #FFFFFF;
                    color: #24943A;
                    padding: 2px 8px;
                    border-radius: 5px;
                }
</style>
