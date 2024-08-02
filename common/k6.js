// https://k6.io/
// brew install k6
// cd common
// k6 run k6.js

import http from 'k6/http';

export let options = {
    vus: 15, // количество юзеров
    duration: '30s' // длина теста
};

function getRandomBoolean() {
    return Math.random() < 0.5; // для рандома OnlyCached
}

export default function () {
    let url = 'http://localhost:5260/v1/routes';
    let payload = JSON.stringify({
        "origin": "Moscow",
        "destination": "Sochi",
        "originDateTime": "2024-08-07T17:29:32.200Z",
        "destinationDateTime": "2024-08-12T17:29:32.200Z",
        "maxPrice": 123,
        "minTimeLimit": "2024-08-02T17:29:32.200Z",
        "onlyCached": getRandomBoolean()
    });

    let params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    let res = http.post(url, payload, params);
    
    console.log(`Response status code: ${res.status}, onlyCached: ${payload.onlyCached}`);
}