interface TypeObjectProperties {
    id: number;
    questionaryObjectTypeId: number;
    name: string;
}
const FormDialogObject = (props) => {
    const dialogForm = useForm();
    const {register, handleSubmit, control} = dialogForm;

    const onSubmit = data => {
        console.log(data);
        data.a = '1';
        const response = fetch("/api/ObjectTypeApi", {
            method: "POST",
            headers: {"Content-Type": "application/json"},
            credentials: "include",
            body: JSON.stringify(data)
        })
       
    };
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
                Создать новый объект
            </Button>
            <Dialog fullWidth={true} maxWidth={'lg'} open={open} onClose={handleClose} aria-labelledby="form-dialog-title">
                <DialogTitle id="form-dialog-title">Новый объект</DialogTitle>
                <form onSubmit={handleSubmit(onSubmit)} autoComplete="off">
                    <DialogContent>
                        <Controller
                            as={TextField}
                            autoFocus
                            name="name"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Название"
                            fullWidth={true}
                        />
                        <Controller
                            as={TextField}
                            name="code"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-required"
                            label="Код"
                            fullWidth={true}
                        />
                        <Controller
                            as={TextField}
                            name="description"
                            control={control}
                            defaultValue=""
                            required
                            margin="dense"
                            id="standard-multiline-static"
                            label="Описание"
                            fullWidth={true}
                            multiline
                            rows={4}
                        />
                        {/*<TextField*/}
                        {/*    required*/}
                        {/*    id="standard-multiline-static"*/}
                        {/*    label=""*/}
                        {/*    multiline*/}
                        {/*    rows={4}*/}
                        {/*    fullWidth*/}
                        {/*/>*/}
                    </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose} color="primary">
                        Отмена
                    </Button>
                    <Button type="submit" color="primary">
                        Добавить
                    </Button>
                </DialogActions>
                </form>
            </Dialog>
        </div>
    );
}