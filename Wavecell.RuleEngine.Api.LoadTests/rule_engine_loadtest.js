import http from 'k6/http';
import { sleep } from 'k6';

export let options = {
    noConnectionReuse: false,
    vus: 120,
    duration: '10s'
}

export default () => {
    http.get('https://localhost:5001/rule-sets/strings/rules?s1=AAA&s2=BBB&s3=CCC&s4=AAA');
    // sleep(1);
};