const SlideTransition = React.forwardRef(function Transition(
    props: any & { children?: React.ReactElement<any, any> },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="up" ref={ref} {...props} />;
});

function CloseAlertDialog(props: {getAllRoute:string}) {
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = redirectToAll => {
        setOpen(false);
        
        if(redirectToAll === true){
            window.location = props.getAllRoute as any;
        }
    };

    return (
        <div className="d-flex justify-content-around">
            <Button className="d-flex justify-content-end" variant="outlined" color="secondary" onClick={handleClickOpen}>
                Выйти без сохранения
            </Button>
            <Dialog
                open={open}
                TransitionComponent={SlideTransition}
                keepMounted
                onClose={handleClose}
                aria-labelledby="alert-dialog-slide-title"
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle id="alert-dialog-slide-title">{"Анкета не будет сохранена, выйти?"}</DialogTitle>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Нет
                    </Button>
                    <Button onClick={() => handleClose(true)} color="primary">
                        Да
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}