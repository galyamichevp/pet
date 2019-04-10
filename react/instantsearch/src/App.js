import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import 'instantsearch.css/themes/algolia.css';
import { InstantSearch, SearchBox, Hits, Highlight } from 'react-instantsearch-dom';

class Hitx extends Component {
  render() {
    const x = this.props;
    const { hit } = this.props;
    return (
      <div className="hit">
        <div className="hit-image">
          <img src={hit.image} />
        </div>
        <div className="hit-name">
          <Highlight attribute="name" hit={hit}></Highlight>
        </div>
      </div>
    );
  }
}

const Hit = ({ hit }) => (
  <div className="hit">
    <div className="hit-image">
      <img src={hit.image} />
    </div>
    <div className="hit-name">
      <Highlight attribute="name" hit={hit}></Highlight>
    </div>
  </div>);

const Content = () =>
  <div className="content">
    <Hits hitComponent={Hitx} />
  </div>

class App extends Component {
  render() {
    return (
      <div className="App">
        <header className="App-header">
          Learn React
        </header>
        <InstantSearch
          appId="latency"
          apiKey="3d9875e51fbd20c7754e65422f7ce5e1"
          indexName="bestbuy"
        >
          <SearchBox translations={{ placeholder: 'Search Box' }} />
          <main>
            <Content />
          </main>
        </InstantSearch>
      </div>
    );
  }
}

export default App;
