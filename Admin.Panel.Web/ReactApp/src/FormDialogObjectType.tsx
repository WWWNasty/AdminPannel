function FormDialogObjectType(props) {
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
                <DialogTitle id="form-dialog-title">Новый тип объекта</DialogTitle>
                <DialogContent>
                    <div><MySelect selectOptions = {props.selectOptions} selectedValue = {props.selectedValue} setSelectedValue = {props.setSelectedValue} nameSwlect = "Выберите компанию" /></div>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="name"
                        label="Название"
                        fullWidth
                    />
                    <CardProp/>
                    <div>
                        <IconButton color="primary" aria-label="add">
                            <Icon>add</Icon>
                        </IconButton>
                    </div>
                   
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