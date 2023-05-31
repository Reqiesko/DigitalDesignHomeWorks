const currencyForm = document.getElementById('currencyForm');
currencyForm.addEventListener('submit', async (event) => {
        event.preventDefault();

        const USDInput = document.getElementById('USD');
        const EURInput = document.getElementById('EUR');

        try {
            const response = await fetch('https://www.cbr-xml-daily.ru/daily_json.js');
            const data = await response.json();
            
            const USDRate = data.Valute.USD.Value;
            const EURRate = data.Valute.EUR.Value;
            
            USDInput.value = USDRate.toFixed(2);
            EURInput.value = EURRate.toFixed(2);
        } catch (error) {
            console.log('Ошибка при получении курсов валют:', error);
        }
    });

const cryptoCurrencyForm = document.getElementById('cryptoCurrencyForm');
 cryptoCurrencyForm.addEventListener('submit', async (event) => {
        event.preventDefault(); 
    
        const btcValue = document.getElementById('BTC');
        const dogeValue = document.getElementById('Doge');
        const ethValue = document.getElementById('ETH');
        const usdtValue = document.getElementById('USDT');
    
        try {
            const response = await fetch('https://api.coincap.io/v2/assets?ids=bitcoin,dogecoin,ethereum,tether');
            const data = await response.json();
    
            const btcPrice = data.data[0].priceUsd;
            const dogePrice = data.data[1].priceUsd;
            const ethPrice = data.data[2].priceUsd;
            const usdtPrice = data.data[3].priceUsd;
    
            btcValue.value = btcPrice;
            dogeValue.value = dogePrice;
            ethValue.value = ethPrice;
            usdtValue.value = usdtPrice;
    
        } catch (error) {
            console.log('Ошибка при получении данных:', error);
        }
    });