<template>
    <v-wait for="fetching stats">
        <template slot="waiting">
            <div class="loader">
                <span class="animated">️️♻️</span>Загрузка...
            </div>
        </template>
        <div v-if="game">
            <div class="row h3">
                <div class="col text-left">
                    <b-link :to="{name: 'games-date', params:{date: this.$moment(game.dateStart).format('DD-MM-YYYY')}}">
                        {{ game.dateStart | moment("DD.MM.YYYY") }}
                    </b-link>{{ game.dateStart | moment("HH:mm") }}
                </div>
                <div class="col text-right">
                    {{ game.stage.title }}
                </div>
            </div>
            <div class="row">
                <div class="col text-right">

                </div>
                <div class="col-1 text-right border-right">
                    <h3>

                    </h3>
                </div>
                <!--<div class="col-1 text-center"><h3>:</h3></div>-->
                <div class="col-1 text-left">

                </div>
                <div class="col text-left">
                    <h3></h3>
                </div>
            </div>
            <table class="table table-striped">
                <thead class="thead-dark">
                    <tr>
                        <th colspan="3" class="col-3 h3 text-right">{{ game.teamHome.title }}</th>
                        <th colspan="1" class="col-1 h3 text-center border-right">
                            <b>{{ game.periods[4].home }}</b>
                        </th>
                        <th colspan="1" class="col-1 h3 text-center">
                            <b>{{ game.periods[4].away }}</b>
                        </th>
                        <th colspan="3" class="col-3 h3 text-left">{{ game.teamAway.title }}</th>
                    </tr>
                </thead>
                <thead>
                    <tr>
                        <th colspan="2" class="col-2 text-center border-right">1 период</th>
                        <th colspan="2" class="col-2 text-center border-right">2 период</th>
                        <th colspan="2" class="col-2 text-center border-right">3 период</th>
                        <th colspan="2" class="col-2 text-center">4 период</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[0].home }}</td>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[0].away }}</td>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[1].home }}</td>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[1].away }}</td>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[2].home }}</td>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[2].away }}</td>
                        <td colspan="1" class="col-1 h5 text-center border-right">{{ game.periods[3].home }}</td>
                        <td colspan="1" class="col-1 h5 text-center">{{ game.periods[3].away }}</td>
                    </tr>
                    <tr>
                        <td colspan="2" class="text-center border-right">
                            <div v-if="round>=1" class="btn-group btn-group-toggle" data-toggle="buttons">
                                <label class="btn" v-bind:class="p1===null?'btn-secondary active':'btn-outline-secondary'">
                                    <input type="radio" name="p1" v-model="p1" v-bind:value="null">Неизв.
                                </label>
                                <label class="btn" v-bind:class="p1===0?'btn-primary active':'btn-outline-primary'">
                                    <input type="radio" name="p1" v-model="p1" v-bind:value="0">Четный
                                </label>
                                <label class="btn" v-bind:class="p1===1?'btn-success active':'btn-outline-success'">
                                    <input type="radio" name="p1" v-model="p1" v-bind:value="1">Нечет.
                                </label>
                            </div><span v-if="round===1"><br><small>(нечет: {{ rasklad.p1.oddAway }} игр назад)</small></span>
                        </td>
                        <td colspan="2" class="text-center border-right">
                            <div v-if="round===2||round===3||round===4" class="btn-group btn-group-toggle" data-toggle="buttons">
                                <label class="btn" v-bind:class="p2===null?'btn-secondary active':'btn-outline-secondary'">
                                    <input type="radio" name="p2" v-model="p2" v-bind:value="null">Неизв.
                                </label>
                                <label class="btn" v-bind:class="p2===0?'btn-primary active':'btn-outline-primary'">
                                    <input type="radio" name="p2" v-model="p2" v-bind:value="0">Четный
                                </label>
                                <label class="btn" v-bind:class="p2===1?'btn-success active':'btn-outline-success'">
                                    <input type="radio" name="p2" v-model="p2" v-bind:value="1">Нечет.
                                </label>
                            </div><span v-if="round===2"><br><small>(нечет: {{ rasklad.p2.oddAway }} игр назад)</small></span>
                            <div v-if="round<=1">нечет: {{ rasklad.p2.oddAway }} игр назад</div>
                        </td>
                        <td colspan="2" class="text-center border-right">
                            <div v-if="round===3||round===4" class="btn-group btn-group-toggle" data-toggle="buttons">
                                <label class="btn" v-bind:class="p3===null?'btn-secondary active':'btn-outline-secondary'">
                                    <input type="radio" name="p3" v-model="p3" v-bind:value="null">Неизв.
                                </label>
                                <label class="btn" v-bind:class="p3===0?'btn-primary active':'btn-outline-primary'">
                                    <input type="radio" name="p3" v-model="p3" v-bind:value="0">Четный
                                </label>
                                <label class="btn" v-bind:class="p3===1?'btn-success active':'btn-outline-success'">
                                    <input type="radio" name="p3" v-model="p3" v-bind:value="1">Нечет.
                                </label>
                            </div><span v-if="round===3"><br><small>(нечет: {{ rasklad.p3.oddAway }} игр назад)</small></span>
                            <div  v-if="round<=2">нечет: {{ rasklad.p3.oddAway }} игр назад</div>
                        </td>
                        <td colspan="2" class="text-center border-right">
                            <div v-if="round===4" class="btn-group btn-group-toggle" data-toggle="buttons">
                                <label class="btn" v-bind:class="p4===null?'btn-secondary active':'btn-outline-secondary'">
                                    <input type="radio" name="p4" v-model="p4" v-bind:value="null">Неизв.
                                </label>
                                <label class="btn" v-bind:class="p4===0?'btn-primary active':'btn-outline-primary'">
                                    <input type="radio" name="p4" v-model="p4" v-bind:value="0">Четный
                                </label>
                                <label class="btn" v-bind:class="p4===1?'btn-success active':'btn-outline-success'">
                                    <input type="radio" name="p4" v-model="p4" v-bind:value="1">Нечет.
                                </label>
                            </div><span v-if="round===4"><br><small>(нечет: {{ rasklad.p4.oddAway }} игр назад)</small></span>
                            <div  v-if="round<=3">нечет: {{ rasklad.p4.oddAway }} игр назад</div>
                        </td>
                    </tr>
                </tbody>
                <thead class="thead-dark">
                    <tr>
                        <th colspan="3" class="h5 text-right">Расклад по периодам был игр назад:</th>
                        <th colspan="1" class="h5 text-center font-weight-bold border-right">{{rasklad.prevUniq}}</th>
                        <th colspan="3" class="h5 text-right">Тотал был нечётным игр назад:</th>
                        <th colspan="1" class="h5 text-center font-weight-bold">{{ rasklad.tt.oddAway }}</th>
                    </tr>
                </thead>
            </table>

            <h3>Статистика:</h3>
            <div>
                <b-form-checkbox id="checkbox1" v-model="show1">
                    1 период
                </b-form-checkbox>
                <b-form-checkbox id="checkbox2" v-model="show2">
                    2 период
                </b-form-checkbox>
                <b-form-checkbox id="checkbox3" v-model="show3">
                    3 период
                </b-form-checkbox>
                <b-form-checkbox id="checkbox4" v-model="show4">
                    4 период
                </b-form-checkbox>

                <b-form-checkbox id="checkboxtt" v-model="showtt">
                    тотал
                </b-form-checkbox>

                <b-form-checkbox id="checkboxavg" v-model="showavg">
                    процент
                </b-form-checkbox>
                <b-form-checkbox id="checkboxper" v-model="showper">
                    частота
                </b-form-checkbox>
                <b-form-checkbox id="checkboxsqr" v-model="showsqr">
                    отклонение
                </b-form-checkbox>

            </div>
            <table class="table table-striped table-bordered">
                <thead>
                    <tr>
                        <th rowspan="2">Период</th>
                        <th rowspan="2">Игр</th>
                        <th class="text-center" v-if="show1" :colspan="colsp">1 четв</th>
                        <th class="text-center" v-if="show2" :colspan="colsp">2 четв</th>
                        <th class="text-center" v-if="show3" :colspan="colsp">3 четв</th>
                        <th class="text-center" v-if="show4" :colspan="colsp">4 четв</th>
                        <th class="text-center" v-if="showtt" :colspan="colsp">Тотал</th>

                    </tr>
                    <tr>
                        <th class="text-center" v-if="p1avg">%</th>
                        <th class="text-center" v-if="p1per">Ч</th>
                        <th class="text-center" v-if="p1sqr">с/о</th>
                        <th class="text-center" v-if="p2avg">%</th>
                        <th class="text-center" v-if="p2per">Ч</th>
                        <th class="text-center" v-if="p2sqr">с/о</th>
                        <th class="text-center" v-if="p3avg">%</th>
                        <th class="text-center" v-if="p3per">Ч</th>
                        <th class="text-center" v-if="p3sqr">с/о</th>
                        <th class="text-center" v-if="p4avg">%</th>
                        <th class="text-center" v-if="p4per">Ч</th>
                        <th class="text-center" v-if="p4sqr">с/о</th>
                        <th class="text-center" v-if="ttavg">%</th>
                        <th class="text-center" v-if="ttper">Ч</th>
                        <th class="text-center" v-if="ttsqr">с/о</th>
                    </tr>
                </thead>
                <tbody :key="time.p1stats" v-for="time in rasklad.statistics">
                    <tr>
                        <td>{{time.descr}}</td>
                        <td>{{time.count}}</td>
                        <td class="text-center" v-if="p1avg">{{time.p1.perc.toFixed(2)}}</td>
                        <td class="text-center" v-if="p1per">{{time.p1.freq.toFixed(2)}}</td>
                        <td class="text-center" v-if="p1sqr">{{time.p1.sqrt.toFixed(2)}}</td>
                        <td class="text-center" v-if="p2avg">{{time.p2.perc.toFixed(2)}}</td>
                        <td class="text-center" v-if="p2per">{{time.p2.freq.toFixed(2)}}</td>
                        <td class="text-center" v-if="p2sqr">{{time.p2.sqrt.toFixed(2)}}</td>
                        <td class="text-center" v-if="p3avg">{{time.p3.perc.toFixed(2)}}</td>
                        <td class="text-center" v-if="p3per">{{time.p3.freq.toFixed(2)}}</td>
                        <td class="text-center" v-if="p3sqr">{{time.p3.sqrt.toFixed(2)}}</td>
                        <td class="text-center" v-if="p4avg">{{time.p4.perc.toFixed(2)}}</td>
                        <td class="text-center" v-if="p4per">{{time.p4.freq.toFixed(2)}}</td>
                        <td class="text-center" v-if="p4sqr">{{time.p4.sqrt.toFixed(2)}}</td>
                        <td class="text-center" v-if="ttavg">{{time.tt.perc.toFixed(2)}}</td>
                        <td class="text-center" v-if="ttper">{{time.tt.freq.toFixed(2)}}</td>
                        <td class="text-center" v-if="ttsqr">{{time.tt.sqrt.toFixed(2)}}</td>
                    </tr>
                </tbody>
            </table>
        </div>
    </v-wait>
</template>

<script>
export default {
  data() {
    return {
      error: null,
      show1: true,
      show2: true,
      show3: true,
      show4: true,
      showot: true,
      showtt: true,
      showavg: true,
      showper: true,
      showsqr: true,
      game: null,
      rasklad: null,
      p1stats: null,
      p2stats: null,
      p3stats: null,
      p4stats: null,
      ttstats: null,
      colsp: 3,
      p1: null,
      p2: null,
      p3: null,
      p4: null
    };
  },
  computed: {
    round: function() {
      if (this.p1 === null) return 1;
      else if (this.p1 !== null && this.p2 === null) return 2;
      else if (this.p1 !== null && this.p2 !== null && this.p3 === null)
        return 3;
      else if (
        this.p1 !== null &&
        this.p2 !== null &&
        this.p3 !== null &&
        this.p4 === null
      )
        return 4;
      else return 5;
    },
    p1avg: function() {
      return this.show1 && this.showavg;
    },
    p1per: function() {
      return this.show1 && this.showper;
    },
    p1sqr: function() {
      return this.show1 && this.showsqr;
    },
    p2avg: function() {
      return this.show2 && this.showavg;
    },
    p2per: function() {
      return this.show2 && this.showper;
    },
    p2sqr: function() {
      return this.show2 && this.showsqr;
    },
    p3avg: function() {
      return this.show3 && this.showavg;
    },
    p3per: function() {
      return this.show3 && this.showper;
    },
    p3sqr: function() {
      return this.show3 && this.showsqr;
    },
    p4avg: function() {
      return this.show4 && this.showavg;
    },
    p4per: function() {
      return this.show4 && this.showper;
    },
    p4sqr: function() {
      return this.show4 && this.showsqr;
    },
    ttavg: function() {
      return this.showtt && this.showavg;
    },
    ttper: function() {
      return this.showtt && this.showper;
    },
    ttsqr: function() {
      return this.showtt && this.showsqr;
    }
  },
  created() {
    this.fetchData();
  },
  watch: {
    $route: "fetchData",
    showavg: "colspan",
    showper: "colspan",
    showsqr: "colspan",
    p1: function() {
      if (this.p1 === null) this.p2 = this.p3 = this.p4 = null;
      else this.show1 = false;
      return this.perCheck();
    },
    p2: function() {
      if (this.p2 === null) this.p3 = this.p4 = null;
      else this.show2 = false;
      return this.perCheck();
    },
    p3: function() {
      if (this.p3 === null) this.p4 = null;
      else this.show3 = false;
      return this.perCheck();
    },
    p4: function() {
      if (this.p4 !== null) this.show4 = false;
    }
  },
  methods: {
    perCheck() {
      this.$router.replace({
        name: "game-stats",
        params: {
          id: this.game.id,
          p1: this.p1 === null ? undefined : this.p1,
          p2: this.p2 === null ? undefined : this.p2,
          p3: this.p3 === null ? undefined : this.p3,
          p4: this.p4 === null ? undefined : this.p4
        }
      });
    },
    colspan() {
      var ret = 0;
      if (this.showavg === true) ret++;
      if (this.showper === true) ret++;
      if (this.showsqr === true) ret++;
      if (ret === 0) ret = 1;
      this.colsp = ret;
    },
    fetchData() {
      this.$wait.start("fetching stats");
      this.error = this.game = this.rasklad = this.p1stats = this.p2stats = this.p3stats = this.p4stats = this.ttstats = this.p1 = this.p2 = this.p3 = this.p4 = null;
      this.$http
        .get(
          "/api/SampleData/GetStatsForGame/" +
            this.$route.params.id +
            "?p1=" +
            this.$route.params.p1 +
            "&p2=" +
            this.$route.params.p2 +
            "&p3=" +
            this.$route.params.p3 +
            "&p4=" +
            this.$route.params.p4
        )
        .then(response => {
          console.log(response.data);
          this.game = response.data.game;
          this.rasklad = response.data.rasklad;
          this.p1 =
            response.data.rasklad.p1.oddUser === undefined
              ? null
              : response.data.rasklad.p1.oddUser;
          this.p2 =
            response.data.rasklad.p2.oddUser === undefined
              ? null
              : response.data.rasklad.p2.oddUser;
          this.p3 =
            response.data.rasklad.p3.oddUser === undefined
              ? null
              : response.data.rasklad.p3.oddUser;
          this.p4 =
            response.data.rasklad.p4.oddUser === undefined
              ? null
              : response.data.rasklad.p4.oddUser;
          this.$wait.end("fetching stats");
        })
        .catch(error => {
          this.loading = false;
          console.log(error);
          this.$wait.end("fetching stats");
        });
    }
  }
};
</script>

<style>
</style>
