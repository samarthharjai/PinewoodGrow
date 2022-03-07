

const labels = [
    "0", "5000", "10000", "15000", "20000", "25000", "30000", "35000", "40000", "45000", "50000", "55000", "60000"]

const data = {
    //labels,
    dataSets: [
        {
            data: [
                { x: 5, y: 10 },
                { x: 4, y: 10 },
                { x: 2, y: 10 },
                { x: 5, y: 15 },
                { x: 1, y: 19 },
                { x: 4, y: 4 }
            ],
            backgroundColor: 'rgba(54,162,235,0.2)',
            borderColor: 'rgba(54,162,235,1)',
            borderWidth: 1,
            /*showLine: true,
            tension: 0.4*/
        }]
}
const config = {
    type: 'scatter',
    data,
    options: {
        scales: {
            y: {
                beginAtZero: true
            }
        }
    }
};

const myChart = new Chart(
    document.getElementById('myChart'),
    config
);