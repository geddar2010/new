<template>
    <v-wait for="fetching stats">
        <template slot="waiting">
            <div class="loader">
                <span class="animated">️️♻️</span>Загрузка...
            </div>
        </template>
        <div v-if="game">
            <h3>{{ game.stage.title }}</h3>
            <h3 class="pull-right">{{ game.dateStart | moment("DD.MM.YYYY HH:mm") }}</h3>
            <table class="table table-striped ">
                <thead>
                    <tr>
                        <th>Дом</th>
                        <th>Гость</th>
                        <th>1</th>
                        <th>2</th>
                        <th>3</th>
                        <th>4</th>
                        <th>OT</th>
                        <th>TT</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{{ game.teamHome.title }}</td>
                        <td>{{ game.teamAway.title }}}</td>
                        <td>{{ game.periods[0].home }} : {{ game.periods[0].away }}</td>
                        <td>{{ game.periods[1].home }} : {{ game.periods[1].away }}</td>
                        <td>{{ game.periods[2].home }} : {{ game.periods[2].away }}</td>
                        <td>{{ game.periods[3].home }} : {{ game.periods[3].away }}</td>
                        <td>{{ game.periods[4].home }} : {{ game.periods[4].away }}</td>
                        <td>{{ game.periods[5].home }} : {{ game.periods[5].away }}</td>
                    </tr>
                    <tr>
                        <td>Уникальность ситуации:{{ rasklad.prevUniq }}</td>
                        <td>Нечёт игр назад:</td>
                        <td>{{ rasklad.p1.oddAway }}</td>
                        <td>{{ rasklad.p2.oddAway }}</td>
                        <td>{{ rasklad.p3.oddAway }}</td>
                        <td>{{ rasklad.p4.oddAway }}</td>
                        <td></td>
                        <td>{{ rasklad.tt.oddAway }}</td>
                    </tr>
                </tbody>
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
            <table class="table table-striped ">
                <thead>
                    <tr>
                        <th rowspan="2">Период</th>
                        <th rowspan="2">Игр</th>
                        <th v-if="show1" :colspan="colsp">1 четв</th>
                        <th v-if="show2" :colspan="colsp">2 четв</th>
                        <th v-if="show3" :colspan="colsp">3 четв</th>
                        <th v-if="show4" :colspan="colsp">4 четв</th>
                        <th v-if="showtt" :colspan="colsp">Тотал</th>

                    </tr>
                    <tr>
                        <th v-if="p1avg">%</th>
                        <th v-if="p1per">Ч</th>
                        <th v-if="p1sqr">с/о</th>
                        <th v-if="p2avg">%</th>
                        <th v-if="p2per">Ч</th>
                        <th v-if="p2sqr">с/о</th>
                        <th v-if="p3avg">%</th>
                        <th v-if="p3per">Ч</th>
                        <th v-if="p3sqr">с/о</th>
                        <th v-if="p4avg">%</th>
                        <th v-if="p4per">Ч</th>
                        <th v-if="p4sqr">с/о</th>
                        <th v-if="ttavg">%</th>
                        <th v-if="ttper">Ч</th>
                        <th v-if="ttsqr">с/о</th>
                    </tr>
                </thead>
                <tbody :key="time.p1stats" v-for="time in rasklad.statistics">
                    <tr>
                        <td>{{time.descr}}</td>
                        <td>{{time.count}}</td>
                        <td v-if="p1avg">{{time.p1.perc.toFixed(2)}}</td>
                        <td v-if="p1per">{{time.p1.freq.toFixed(2)}}</td>
                        <td v-if="p1sqr">{{time.p1.sqrt.toFixed(2)}}</td>
                        <td v-if="p2avg">{{time.p2.perc.toFixed(2)}}</td>
                        <td v-if="p2per">{{time.p2.freq.toFixed(2)}}</td>
                        <td v-if="p2sqr">{{time.p2.sqrt.toFixed(2)}}</td>
                        <td v-if="p3avg">{{time.p3.perc.toFixed(2)}}</td>
                        <td v-if="p3per">{{time.p3.freq.toFixed(2)}}</td>
                        <td v-if="p3sqr">{{time.p3.sqrt.toFixed(2)}}</td>
                        <td v-if="p4avg">{{time.p4.perc.toFixed(2)}}</td>
                        <td v-if="p4per">{{time.p4.freq.toFixed(2)}}</td>
                        <td v-if="p4sqr">{{time.p4.sqrt.toFixed(2)}}</td>
                        <td v-if="ttavg">{{time.tt.perc.toFixed(2)}}</td>
                        <td v-if="ttper">{{time.tt.freq.toFixed(2)}}</td>
                        <td v-if="ttsqr">{{time.tt.sqrt.toFixed(2)}}</td>
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
    showsqr: "colspan"
  },
  methods: {
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
        .get("/api/SampleData/GetStatsForGame/" + this.$route.params.id)
        .then(response => {
          console.log(response.data);
          this.game = response.data.game;
          this.rasklad = response.data.rasklad;
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
