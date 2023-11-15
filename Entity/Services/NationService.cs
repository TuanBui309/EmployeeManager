using Entity.Constants;
using Entity.Models;
using Entity.Repository.Repositories;
using Entity.Respository.Respositories;
using Entity.Services.Interface;
using Entity.Services.ViewModels;

namespace Entity.Services
{

    public class NationService : INationService
    {
        INationRepository _nationRepository;
        public NationService(INationRepository nationRepository) : base()
        {
            _nationRepository = nationRepository;
        }
        public async Task<ResponseEntity> DeleteNation(int id)
        {
            try
            {
                var nation = await _nationRepository.GetSingleByIdAsync(x => x.Id == id);
                if (nation == null)
                {
                    return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);

                }
                await _nationRepository.DeleteAsync(nation);
                return new ResponseEntity(StatusCodeConstants.OK, nation, MessageConstants.DELETE_SUCCESS);

            }
            catch(Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.DELETE_ERROR);
            }
        }

        public async Task<ResponseEntity> GetAllNation()
        {
            var nations = await _nationRepository.GetAllAsync();
            return new ResponseEntity(StatusCodeConstants.OK, nations, MessageConstants.MESSAGE_SUCCESS_200);

        }

        public async Task<ResponseEntity> GetSingleNationById(int id)
        {
            try
            {
                var nation = await _nationRepository.GetSingleByIdAsync(x => x.Id == id);
                if (nation == null)
                {
                    return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
                }
                return new ResponseEntity(StatusCodeConstants.OK, nation, MessageConstants.MESSAGE_SUCCESS_200);
            }
            catch(Exception ex)
            {
                return new(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.MESSAGE_ERROR_404);
            }
        }

        public async Task<ResponseEntity> InsertNation(InsertNation model)
        {
            try
            {
                Nation nations = new Nation();
                nations.NationName = model.NationName;
                await _nationRepository.InsertAsync(nations);
                return new(StatusCodeConstants.OK, nations, MessageConstants.INSERT_SUCCESS);


            }
            catch(Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.INSERT_ERROR);
            }
        }

        public async Task<ResponseEntity> UpdateNation(UpdateNation model)
        {
            try
            {
                var nation = await _nationRepository.GetSingleByIdAsync(x => x.Id == model.Id);
                if (nation == null)
                {
                    return new ResponseEntity(StatusCodeConstants.NOT_FOUND, "", MessageConstants.MESSAGE_ERROR_404);
                }
                await _nationRepository.UpdateAsync(nation, nation);
                return new ResponseEntity(StatusCodeConstants.OK, nation, MessageConstants.UPDATE_SUCCESS);
            }
            catch(Exception ex)
            {
                return new ResponseEntity(StatusCodeConstants.BAD_REQUEST, ex.Message, MessageConstants.UPDATE_ERROR);
            }
        }
    }

}
