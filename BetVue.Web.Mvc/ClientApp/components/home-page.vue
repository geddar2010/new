<template>
    <div>
        <v-wait for="fetching games">
        <template slot="waiting">
            <div class="loader">
            <span class="animated">️️♻️</span>Загрузка...
            </div>
        </template>
        <div>
            <b-button-toolbar key-nav>
                <b-button-group class="mx-2">
                    <b-btn :to="{name:'games-date', params:{date: datePrev }}">&laquo;</b-btn>
                </b-button-group>
                <b-button-group class="mx-2">
                    <b-btn>{{ date }}</b-btn>
                </b-button-group>
                <b-button-group class="mx-2">
                    <b-btn :to="{name:'games-date', params:{date: dateNext}}">&raquo;</b-btn>
                </b-button-group>
            </b-button-toolbar>
        </div>
        <table class="table" v-if="games">
            <thead>
                <tr>
                    <th>Дата</th>
                    <th>Чемпионат</th>
                    <th>Дом</th>
                    <th>Гость</th>
                    <th>Итог</th>
                    <th>Статус</th>
                    <th>Обновление</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="game in games" :key="game.id">
                    <td><b-link :to="{name:'game-stats', params:{id: game.id}}">{{ game.dateStart | moment("DD.MM.YYYY HH:mm") }}</b-link></td>
                    <td>{{ game.stage.title }}</td>
                    <td>{{ game.teamHome.title }}</td>
                    <td>{{ game.teamAway.title }}}</td>
                    <td>{{ game.total.home }} : {{ game.total.away }}</td>
                    <td>{{ game.status }}</td>
                    <td><b-link :to="{name:'game-stats', params:{id: game.id}}">{{ game.dateUpdate | moment("DD.MM.YYYY HH:mm") }}</b-link></td>
                </tr>
            </tbody>
        </table>
    </v-wait>
    </div>
</template>

<script>
export default {
  data() {
    return {
      games: null,
      stats: null,
      date: null,
      dateNext: null,
      datePrev: null
    };
  },
  created() {
    this.fetchData();
  },
  watch: {
    $route: "fetchData"
  },
  methods: {
    fetchData() {
      this.$wait.start("fetching games");
      this.error = this.games = this.stats = this.dateNext = this.datePrev = null;

      var dat =
        this.$route.params.date === undefined ? "" : this.$route.params.date;

      this.$http
        .get("/api/SampleData/GetGamesForDate/" + dat)
        .then(response => {
          this.loading = false;
          console.log(response.data);
          this.games = response.data.games;
          this.date = this.$moment(response.data.date).format("DD.MM.YYYY");
          this.dateNext = this.$moment(response.data.date)
            .add(1, "days")
            .format("DD-MM-YYYY");
          this.datePrev = this.$moment(response.data.date)
            .add(-1, "days")
            .format("DD-MM-YYYY");
          this.$wait.end("fetching games");
        })
        .catch(error => {
          this.$wait.end("fetching games");
          console.log(error);
        });
    }
  }
};
</script>
