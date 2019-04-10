import React, { Component } from 'react';

import Button from '@material-ui/core/Button';
import FormControl from '@material-ui/core/FormControl';
import FormGroup from '@material-ui/core/FormGroup';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogActions from '@material-ui/core/DialogActions';
import DialogContent from '@material-ui/core/DialogContent';
import DialogContentText from '@material-ui/core/DialogContentText';

import TextField from '@material-ui/core/TextField';


type SignupData = {
    name: string;
    email: string;
    password: string;
    password_confirmation: string;
    open: boolean;
}

class SignUp extends Component<any, SignupData> {
    constructor(props: any) {
        super(props);
        
        this.state = ({
            name: '',
            email: '',
            password: '',
            password_confirmation: '',
            open: this.props.isOpen
        } as SignupData);

        this.handleClose = this.props.handleClose;
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    handleInputChange = (event: any) => {
        this.setState({
            [event.target.name]: event.target.value
        } as SignupData);
    }

    handleSubmit = (event: any) => {
        event.preventDefault();

        console.log(this.state);

        this.handleClose();
    }

    

    handleClose = () => {
        this.setState({ open: false });
    };

    render() {
        const { isOpen } = this.props

        return (
            <div>

                <Dialog
                    open={this.state.open}
                    onClose={this.props.handleClose}
                    aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Sign-Up</DialogTitle>

                    <DialogContent>
                        <DialogContentText>
                            Register
                        </DialogContentText>

                        <TextField
                            autoFocus
                            margin="dense"
                            id="name"
                            label="Name"
                            type="name"
                            fullWidth
                        />
                        <TextField
                            margin="dense"
                            id="email"
                            label="Email Address"
                            type="email"
                            fullWidth
                        />
                        <TextField
                            margin="dense"
                            id="password"
                            label="Password"
                            type="password"
                            fullWidth
                        />
                        <TextField
                            margin="dense"
                            id="password_confirmation"
                            label="Password Confirmation"
                            type="password"
                            fullWidth
                        />
                    </DialogContent>

                    <DialogActions>
                        <Button onClick={this.props.handleClose} color="primary">
                            Cancel
                        </Button>
                        <Button onClick={this.handleSubmit} color="primary">
                            Submit
                        </Button>
                    </DialogActions>
                </Dialog>
            </div>
        );
    }
}

export default SignUp;