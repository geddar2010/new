import GameStats from 'components/game-stats';
import CounterExample from 'components/counter-example';
import FetchData from 'components/fetch-data';
import HomePage from 'components/home-page';

export const routes = [{
        path: '/',
        component: HomePage,
        display: 'Home',
        style: 'glyphicon glyphicon-home'
    },
    {
        path: '/date/:date',
        name: 'games-date',
        component: HomePage,
        display: 'На дату',
        style: 'glyphicon glyphicon-education'
    },
    {
        path: '/counter',
        component: CounterExample,
        display: 'Counter',
        style: 'glyphicon glyphicon-education'
    },
    {
        path: '/fetch-data',
        component: FetchData,
        display: 'Fetch data',
        style: 'glyphicon glyphicon-th-list'
    },
    {
        path: '/game-stats/:id/:p1?/:p2?/:p3?/:p4?',
        name: 'game-stats',
        component: GameStats,
        display: 'Статистика',
        style: 'glyphicon glyphicon-th-list'
    }
]
