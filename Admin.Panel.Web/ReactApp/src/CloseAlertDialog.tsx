const Transition = React.forwardRef(function Transition(
    props: any & { children?: React.ReactElement<any, any> },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="up" ref={ref} {...props} />;
});

function CloseAlertDialog() {
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    return (
        <div className="mt-3">
            <Button variant="outlined" color="secondary" onClick={handleClickOpen}>
                Отмена
            </Button>
            <Dialog
                open={open}
                TransitionComponent={Transition}
                keepMounted
                onClose={handleClose}
                aria-labelledby="alert-dialog-slide-title"
                aria-describedby="alert-dialog-slide-description"
            >
                <DialogTitle id="alert-dialog-slide-title">{"Анкета не будет сохранена, выйти?"}</DialogTitle>
                {/*<DialogContent>*/}
                {/*    <DialogContentText id="alert-dialog-slide-description">*/}
                {/*        Let Google help apps determine location. This means sending anonymous location data to*/}
                {/*        Google, even when no apps are running.*/}
                {/*    </DialogContentText>*/}
                {/*</DialogContent>*/}
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Нет
                    </Button>
                    <Button onClick={handleClose} color="primary">
                        Да
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}