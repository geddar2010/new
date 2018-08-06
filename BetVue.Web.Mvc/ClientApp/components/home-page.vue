<template>
 <div>
        <h3>Игры на: {{ date }}</h3>
        <p v-if="!games"><em>Загрузка...</em></p>
<!--<v-data-table
    :headers="headers"
    :items="games"
    class="elevation-1"
  >
    <template slot="items" slot-scope="props">
      <td>{{ props.item.date }}</td>
      <td class="text-xs-left">{{ props.item.leaN }}</td>
      <td class="text-xs-right">{{ props.item.thNm }}</td>
      <td class="text-xs-left">{{ props.item.taNm }}</td>
      <td class="text-xs-right">{{ props.item.scTH }}</td>
      <td class="text-xs-left">{{ props.item.scTA }}</td>
      <td class="text-xs-right">{{ props.item.stat }}</td>
    </template>
  </v-data-table>-->

  <!--<div>

    <ul class="nav nav-tabs" role="tablist">
        <li role="presentation" class="active"><a href="#sp" aria-controls="sp" role="tab" data-toggle="tab">Список</a></li>
        <li role="presentation"><a href="#p1" aria-controls="p1" role="tab" data-toggle="tab">1 четверть</a></li>
        <li role="presentation"><a href="#p2" aria-controls="p2" role="tab" data-toggle="tab">2 четверть</a></li>
        <li role="presentation"><a href="#p3" aria-controls="p3" role="tab" data-toggle="tab">3 четверть</a></li>
        <li role="presentation"><a href="#p4" aria-controls="p4" role="tab" data-toggle="tab">4 четверть</a></li>
        <li role="presentation"><a href="#tt" aria-controls="tt" role="tab" data-toggle="tab">Тотал</a></li>
    </ul>

    <div class="tab-content">
        <div role="tabpanel" class="tab-pane" id="sp">

        </div>
        <div role="tabpanel" class="tab-pane active" id="p1">

        </div>
        <div role="tabpanel" class="tab-pane" id="p2">

        </div>
        <div role="tabpanel" class="tab-pane" id="p3">

        </div>
        <div role="tabpanel" class="tab-pane" id="p4">

        </div>
        <div role="tabpanel" class="tab-pane" id="tt">

        </div>
    </div>
</div>-->
<div v-if="stats">
            <table class="table" v-if="stats">
                <thead>
                    <tr>
                        <th colspan="3"><h3><b>1 период</b></h3><h5>нечёт был {{stats.p1awy}} игр назад</h5><th colspan="2"><small  class="pull-right">в таблице данные только по нечетам</small></th>
                    </tr>
                    <tr>
                        <th>За 1 период</th>
                        <th></th>
                        <th>Среднее арифметическое</th>
                        <th>Среднеквадр. отклонение</th>
                        <th>Выпадает каждые N игр</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="stat in stats.stats" :key="stat.descr">
                        <td>{{ stat.descr }}</td>
                        <td>({{ stat.total }} игр)</td>
                        <td>{{ (stat.p1_Avg*100).toFixed(2) }} %</td>
                        <td>{{ (stat.p1_Sqr*100).toFixed(2) }}</td>
                        <td>{{ stat.p1_Per.toFixed(1) }}</td>
                    </tr>
                </tbody>
            </table>
            <table class="table" v-if="stats">
                <thead>
                    <tr>
                        <th colspan="3"><h3><b>2 период</b></h3><h5>нечёт был {{stats.p2awy}} игр назад</h5><th colspan="2"><small  class="pull-right">в таблице данные только по нечетам</small></th>
                    </tr>
                    <tr>
                        <th>За 2 период</th>
                        <th></th>
                        <th>Среднее арифметическое</th>
                        <th>Среднеквадр. отклонение</th>
                        <th>Выпадает каждые N игр</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="stat in stats.stats" :key="stat.descr">
                        <td>{{ stat.descr }}</td>
                        <td>({{ stat.total }} игр)</td>
                        <td>{{ (stat.p2_Avg*100).toFixed(2) }} %</td>
                        <td>{{ (stat.p2_Sqr*100).toFixed(2) }}</td>
                        <td>{{ stat.p2_Per.toFixed(1) }}</td>
                    </tr>
                </tbody>
            </table>
            <table class="table" v-if="stats">
                <thead>
                    <tr>
                        <th colspan="3"><h3><b>3 период</b></h3><h5>нечёт был {{stats.p3awy}} игр назад</h5><th colspan="2"><small  class="pull-right">в таблице данные только по нечетам</small></th>
                    </tr>
                    <tr>
                        <th>За 3 период</th>
                        <th></th>
                        <th>Среднее арифметическое</th>
                        <th>Среднеквадр. отклонение</th>
                        <th>Выпадает каждые N игр</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="stat in stats.stats" :key="stat.descr">
                        <td>{{ stat.descr }}</td>
                        <td>({{ stat.total }} игр)</td>
                        <td>{{ (stat.p3_Avg*100).toFixed(2) }} %</td>
                        <td>{{ (stat.p3_Sqr*100).toFixed(2) }}</td>
                        <td>{{ stat.p3_Per.toFixed(1) }}</td>
                    </tr>
                </tbody>
            </table>
            <table class="table" v-if="stats">
                <thead>
                    <tr>
                        <th colspan="3"><h3><b>4 период</b></h3><h5>нечёт был {{stats.p4awy}} игр назад</h5><th colspan="2"><small  class="pull-right">в таблице данные только по нечетам</small></th>
                    </tr>
                    <tr>
                        <th>За 4 период</th>
                        <th></th>
                        <th>Среднее арифметическое</th>
                        <th>Среднеквадр. отклонение</th>
                        <th>Выпадает каждые N игр</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="stat in stats.stats" :key="stat.descr">
                        <td>{{ stat.descr }}</td>
                        <td>({{ stat.total }} игр)</td>
                        <td>{{ (stat.p4_Avg*100).toFixed(2) }} %</td>
                        <td>{{ (stat.p4_Sqr*100).toFixed(2) }}</td>
                        <td>{{ stat.p4_Per.toFixed(1) }}</td>
                    </tr>
                </tbody>
            </table>
            <table class="table" v-if="stats">
                <thead>
                    <tr>
                        <th colspan="3"><h3><b>Тотал</b></h3><h5>нечёт был {{stats.ttawy}} игр назад</h5><th colspan="2"><small  class="pull-right">в таблице данные только по нечетам</small></th>
                    </tr>
                    <tr>
                        <th>Тотал</th>
                        <th></th>
                        <th>Среднее арифметическое</th>
                        <th>Среднеквадр. отклонение</th>
                        <th>Выпадает каждые N игр</th>
                    </tr>
                </thead>
                <tbody>
                    <tr v-for="stat in stats.stats" :key="stat.descr">
                        <td>{{ stat.descr }}</td>
                        <td>({{ stat.total }} игр)</td>
                        <td>{{ (stat.tT_Avg*100).toFixed(2) }} %</td>
                        <td>{{ (stat.tT_Awy*100).toFixed(2) }}</td>
                        <td>{{ stat.tT_Per.toFixed(1) }}</td>
                    </tr>
                </tbody>
            </table></div>
        <table class="table" v-if="games">
            <thead>
                <tr>
                    <th>Дата</th>
                    <th>Чемпионат</th>
                    <th>Игра</th>
                    <!--<th>Счёт</th>-->
                    <th>Итог</th>
                    <th>Статус</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="game in games" :key="game.id" v-on:click='select(game)'>
                    <td>{{ game.date }}</td>
                    <td>{{ game.leaN }}</td>
                    <td>{{ game.thNm }} - {{ game.taNm }}</td>
                    <!--<td>{{ game.sc1H }} : {{ game.sc1A }}</td>-->
                    <td>{{ game.scTH }} : {{ game.scTA }}</td>
                    <td>{{ game.stat }}</td>
                    <td><button /></td>
                </tr>
            </tbody>
        </table>


    </div>
</template>

<script>
export default {
  data() {
    return {
      headers: [
        {
          text: "Дата",
          align: "left",
          sortable: true,
          value: "date"
        },
        { text: "Чемпионат", value: "leaN" },
        { text: "Дома", value: "thNm" },
        { text: "В гостях", value: "taNm" },
        { text: "Итог", value: "scTH" },
        { text: "Итог", value: "scTA" },
        { text: "Статус", value: "stat" }
      ],
      games: null,
      stats: null,
      date: null
    };
  },

  methods: {
    async select(game) {
      console.log(game.id);
      try {
        this.stats=null;
        let response = await this.$http.get(
          "/api/SampleData/GetStatsForGame/" + game.id
        );
        console.log(response.data);
        this.stats = response.data;
      } catch (error) {
        console.log(error);
      }
    }
  },
  async created() {
    // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
    // TypeScript can also transpile async/await down to ES5
    try {
      let response = await this.$http.get("/api/SampleData/GetGamesForDate");
      console.log(response.data);
      this.games = response.data.games;
      this.date = response.data.dateFormatted;
    } catch (error) {
      console.log(error);
    }
    // Old promise-based approach
    //this.$http
    //    .get('/api/SampleData/WeatherForecasts')
    //    .then(response => {
    //        console.log(response.data)
    //        this.forecasts = response.data
    //    })
    //    .catch((error) => console.log(error))*/
  }
};
</script>

<style>
</style>
