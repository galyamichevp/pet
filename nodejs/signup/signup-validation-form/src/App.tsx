import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';

import SignUp from './components/forms/signup';

import IconButton from '@material-ui/core/IconButton';
import Add from '@material-ui/icons/Add';

class App extends Component<{}, any> {
  constructor(props: any) {
    super(props);

    this.state = {
      showSignUp: false
    }

    this.handleOpen = this.handleOpen.bind(this);
    this.handleClose = this.handleClose.bind(this);
  }

  onSignupClick() {
    this.setState({
      showSignUp: !this.state.showSignUp
    });

    console.log('sign-up opened');
  }

  handleOpen = () => {
    this.setState({ showSignUp: true });
  };

  handleClose = () => {
    this.setState({ showSignUp: false });
  };

  render() {
    let signUp = null;

    if (this.state.showSignUp) {
      signUp = (
        <SignUp isOpen={this.state.showSignUp} handleClose={this.handleClose}></SignUp>
      );
    }
    
    return (
      <div className="App">
        <IconButton onClick={this.handleOpen}>
          <Add />
        </IconButton>
        {signUp}
      </div>
    );
  }
}

export default App;
