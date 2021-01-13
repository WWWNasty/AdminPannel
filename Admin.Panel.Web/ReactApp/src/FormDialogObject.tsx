const FormDialogObject = (props) => {
    const [open, setOpen] = React.useState(false);

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };
 
    return (
        <div>
            <Button variant="outlined" color="primary" onClick={handleClickOpen} className="mt-3 mb-2">
                Создать новый 
            </Button>
            <Dialog fullWidth={true} maxWidth={'lg'} open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Новый объект</DialogTitle>
                <DialogContent>
                    <div><MySelect selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue} nameSwlect = "Выберите компанию" /></div>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="name"
                        label="Название"
                        fullWidth
                    />
                    <TextField
                        margin="dense"
                        id="name"
                        label="Код"
                        fullWidth
                    />
                    <TextField
                        id="standard-multiline-static"
                        label="Описание"
                        multiline
                        rows={4}
                        fullWidth
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Отмена
                    </Button>
                    <Button onClick={handleClose} color="primary">
                        Добавить
                    </Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}