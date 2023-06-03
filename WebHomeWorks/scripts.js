/* Реализация компонента на основе класса */
class PhoneNumberBlock extends React.Component {
  constructor(props) {
      super(props);
      this.state = {
          phoneNumber: '',
      };
  }


  handleClick(event) {
      this.setState({ phoneNumber: event.target.value });
  }

  handleClickSubmit(event) {
      alert('Введенный номер: ' + this.state.phoneNumber);
  }
  render() {
      return (
          <div className="column">
              <form>
                  <p>Определите ваш телефонный номер:</p>
                  <input type="range" id="ratio" min="70000000000" max="89999999999" step="1" onChange={this.handleClick.bind(this)} />
                  <br />
                  <input type="tel" id="phoneNumber" placeholder="(XXX)-XXX-XXXX" value={this.state.phoneNumber} required />
                  <br />
                  <input type="submit" value="Отправить" onClick={this.handleClickSubmit.bind(this)} />
              </form>
          </div>
      );
  }
}

class CheckBoxBlock extends React.Component {

  handleClickSubmit(event) {
      event.preventDefault(); // Предотвращаем отправку формы

      // Получаем все выбранные флажки
      const selectedCheckboxes = Array.from(event.target.elements)
          .filter(element => element.type === 'checkbox' && element.checked)
          .map(element => element.id);

      // Выводим выбранные флажки в alert
      alert(`Выбранные флажки: ${selectedCheckboxes.join(', ')}`);
  }

  render() {
      const options = [
          { id: '1', label: 'C#' },
          { id: '2', label: 'C#' },
          { id: '3', label: 'C#' },
          { id: '4', label: 'C#' }
      ];

      const checkboxes = options.map(option => (
          <p key={option.id}>
              <input type="checkbox" id={option.id} name="csharp" />
              {option.label}
          </p>
      ));

      return (
          <div className="column">
              <form onSubmit={this.handleClickSubmit}>
                  <lable>Выберете лучший язык программирования</lable>
                  {checkboxes}
                  <input type="submit" value="Сделать правильный выбор" />
              </form>
          </div>
      );
  }
}

class RadioBoxBlock extends React.Component {

  handleClickSubmit(event) {
      event.preventDefault(); // Предотвращаем отправку формы

      // Находим выбранный радио
      const selectedRadio = Array.from(event.target.elements)
          .find(element => element.type === 'radio' && element.checked);

      console.log(selectedRadio)
      // Получаем значение выбранного радио
      const selectedValue = selectedRadio ? selectedRadio.value : '';

      // Выводим выбранное радио в alert
      alert(`Выбранное радио: ${selectedValue}`);
  }

  render() {
      const options = [
          { id: '1', label: 'C#' },
          { id: '2', label: 'C# 1' },
          { id: '3', label: 'C# 2' },
          { id: '4', label: 'C# 3' }
      ];

      const radios = options.map(option => (
          <p key={option.id}>
              <input type="radio" id={option.id} name="csharp" value={option.label} />
              {option.label}
          </p>
      ));

      return (
          <div className="column">
              <form onSubmit={this.handleClickSubmit}>
                  <lable>Выберете лучший язык программирования</lable>
                  {radios}
                  <input type="submit" value="Сделать правильный выбор" />
              </form>
          </div>
      );
  }
}

class DatePickBlock extends React.Component {
  handleClickSubmit(event) {
      event.preventDefault(); // Предотвращаем отправку формы

      // Получаем значение выбранной даты
      const selectedDate = event.target.elements.searchdate.value;

      // Выводим выбранную дату в alert
      alert(`Выбранная дата: ${selectedDate}`);
  }
  render() {
      return (
          <div class="column">
              <form onSubmit={this.handleClickSubmit}>
                  <p>
                      <input title="datepick" type="date" name="searchdate" required />
                      <input type="submit" value="Найти" />
                  </p>
              </form>
          </div>
      );
  }
}

class LoginBlock extends React.Component {

  handleClickSubmit(event) {
      event.preventDefault(); // Предотвращаем отправку формы

      // Получаем значения введенного имени и пароля
      const name = event.target.elements.name.value;
      const password = event.target.elements.password.value;

      // Выводим значения введенного имени и пароля в alert
      alert(`Имя: ${name}\nПароль: ${password}`);
  }

  render() {

      return (
          <div class="column">
              <form onSubmit={this.handleClickSubmit}>
                  <p>
                      <label for="name">Имя:</label>
                      <input type="text" placeholder="Введите свой логин" id="name" name="name" required />
                  </p>
                  <p>
                      <label for="password">Пароль:</label>
                      <input type="password" placeholder="Введите пароль" id="password" name="password" required />

                      <input type="submit" value="Отправить" />
                  </p>
              </form>
          </div>
      );
  }
}

class SelectBlock extends React.Component {
  handleClickSubmit(event) {
      event.preventDefault(); // Предотвращаем отправку формы

      // Получаем выбранное значение селекта
      const selectedValue = event.target.elements.subject.value;

      // Выводим выбранное значение в alert
      alert(`Выбрано: ${selectedValue}`);
  }

  render() {
      const options = [
          { id: '1', label: 'C# 5.0' },
          { id: '2', label: 'C# 6.0' },
          { id: '3', label: 'C# 7.0' },
          { id: '4', label: 'C# T1000' }
      ];

      const selectOptions = options.map(option => (
          <option key={option.id} value={option.label}>
              {option.label}
          </option>
      ));

      return (
          <div class="column">
              <form onSubmit={this.handleClickSubmit}>
                  <legend for="subject">Выберете лучший язык программирования</legend>
                  <select id="subject" name="subject">
                      {selectOptions}
                  </select>
                  <p>
                      <input type="submit" value="Отправить" />
                  </p>
              </form>
          </div>
      );
  }
}

class GetCurrencyBlock extends React.Component {

  handleClickSubmit = async (event) => {
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
  }
  render() {
      return (
          <div className="row">
              <fieldset>
                  <legend>Узнать курсы валют</legend>
                  <form onSubmit={this.handleClickSubmit}>
                      <div>
                          <label>USD</label>
                          <input type="text" id="USD" />
                      </div>
                      <div>
                          <label>EUR</label>
                          <input type="text" id="EUR" />
                      </div>
                      <p>
                          <input type="submit" value="Узнать" />
                      </p>
                  </form>
              </fieldset>
          </div>
      );
  }
}

class GetCryptoCurrencyBlock extends React.Component {
  handleClickSubmit = async (event) => {
      event.preventDefault();

      const btcValue = document.getElementById('bitcoin');
      const dogeValue = document.getElementById('dogecoin');
      const ethValue = document.getElementById('ethereum');
      const usdtValue = document.getElementById('tether');

      try {
          const response = await fetch('https://api.coincap.io/v2/assets?ids=bitcoin,dogecoin,ethereum,tether');
          const data = await response.json();

          const currencies = data.data;
          console.log(currencies);
          currencies.forEach((currency) => {
              const { id, priceUsd } = currency;
              const inputValue = document.getElementById(id);
              if (inputValue) {
                  inputValue.value = priceUsd;
              }
          });

      } catch (error) {
          console.log('Ошибка при получении данных:', error);
      }
  }
  render() {
      const currencies = [
          { id: 'bitcoin', label: 'BTC' },
          { id: 'ethereum', label: 'Etherium' },
          { id: 'tether', label: 'Tether' },
          { id: 'dogecoin', label: 'Doge' },
      ];

      const currencyItems = currencies.map((currency) => (
          <div key={currency.id}>
              <label>{currency.label}</label>
              <input type="text" id={currency.id} />
          </div>
      ));
      return (
          <div className="row">
              <fieldset>
                  <legend>Узнать курс криптовалют (в USD)</legend>
                  <form onSubmit={this.handleClickSubmit}>
                      {currencyItems}
                      <p>
                          <input type="submit" value="Узнать" />
                      </p>
                  </form>
              </fieldset>
          </div>
      );
  }
}

class GetChessLeaderboardBlock extends React.Component {

  handleClickSubmit = async (event) => {
      event.preventDefault();
      const chessLeaderboardTextarea = document.getElementById('chessLeaderboard');
      try {
          const response = await fetch('https://api.chess.com/pub/leaderboards');
          const data = await response.json();

          if (data.daily) {
              for (let user in data.daily) {
                  chessLeaderboardTextarea.value += data.daily[user].username + " : " + data.daily[user].score + "\n"
              }
          } else {
              console.log('Поле "daily" не найдено в ответе');
          }
      } catch (error) {
          console.log('Ошибка при получении лидерборда:', error);
      }
  }

  render() {
      return (
          <div className="row">
              <fieldset>
                  <legend>Узнать рейтинг шахматистов</legend>
                  <form onSubmit={this.handleClickSubmit}>
                      <div>
                          <textarea id="chessLeaderboard"></textarea>
                          <input type="submit" value="Узнать" />
                      </div>
                  </form>
              </fieldset>
          </div>
      );
  }
}