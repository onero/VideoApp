namespace VideoRestAPITests
{
    public interface IControllerTest
    {
        void GetAll();
        void GetByExistingId();
        void NotGetByNonExistingId_ReturnNotFound();
        void PostWithValidObject();
        void NotPostWithInvalidObject_ReturnBadRequest();
        void NotPostWithEmptyObject_ReturnBadRequest();
        void UpdateWithValidObject_ReturnOk();
        void NotUpdateWithEmptyObject_ReturnBadRequest();
        void NotUpdateWithInvalidObject_ReturnBadRequest();
        void NotUpdateWithNonExistingId_ReturnNotFound();
        void DeleteByExistingId_ReturnOk();
        void NotDeleteByNonExistingId_ReturnNotFound();
    }
}