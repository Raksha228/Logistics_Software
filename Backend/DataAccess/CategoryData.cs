using Backend.BusinessLogic;
using Backend.Interfaces;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Backend.DataAccess
{
    /// <summary>
    /// Класс для взаимодействия с данными категорий в базе данных.
    /// Реализует базовые операции CRUD (создание, чтение, обновление, удаление)
    /// в таблице <c>table_categories</c>.
    /// </summary>
    public class CategoryData : Category, ICrudCategory
    {
        /// <summary>
        /// Строка подключения к базе данных из конфигурационного файла приложения.
        /// </summary>
        public string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        /// <summary>
        /// Получает все категории из базы данных.
        /// </summary>
        /// <returns>
        /// <see cref="DataTable"/>, содержащая все записи из таблицы категорий.
        /// </returns>
        /// <remarks>
        /// Каждая строка в возвращённой таблице соответствует одной категории из базы данных.
        /// В случае ошибки пользователю будет показано окно с сообщением.
        /// </remarks>
        public DataTable Select()
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM table_categories";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Выводит сообщение об ошибке
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }

        /// <summary>
        /// Вставляет новую категорию в базу данных.
        /// </summary>
        /// <param name="category">Экземпляр <see cref="Category"/>, содержащий данные новой категории.</param>
        /// <returns>
        /// <c>true</c>, если категория успешно добавлена; иначе — <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если переданный параметр <paramref name="category"/> равен <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Все значения, содержащиеся в объекте <paramref name="category"/>, маппятся на соответствующие поля таблицы.
        /// </remarks>
        public bool Insert(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            bool isSucces = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "INSERT INTO table_categories (title, description, added_date, added_by, added_by_name) VALUES (@title, @description, @added_date, @added_by, @added_by_name)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@title", category.Title);
                cmd.Parameters.AddWithValue("@description", category.Description);
                cmd.Parameters.AddWithValue("@added_date", category.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", category.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", category.AddedByName);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSucces = rows > 0;
            }
            catch (Exception ex)
            {
                // Сообщение пользователю в случае ошибки
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSucces;
        }

        /// <summary>
        /// Обновляет данные существующей категории.
        /// </summary>
        /// <param name="category">Экземпляр <see cref="Category"/>, содержащий новые данные категории. Поле <c>Id</c> обязательно для указания, какую строку обновлять.</param>
        /// <returns>
        /// <c>true</c>, если данные были успешно обновлены; иначе — <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="category"/> равен <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Осуществляет обновление записи по полю <c>id</c> категории.
        /// </remarks>
        public bool Update(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "UPDATE table_categories SET title=@title, description=@description, added_date=@added_date, added_by=@added_by, added_by_name=@added_by_name WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@title", category.Title);
                cmd.Parameters.AddWithValue("@description", category.Description);
                cmd.Parameters.AddWithValue("@added_date", category.AddedDate);
                cmd.Parameters.AddWithValue("@added_by", category.AddedBy);
                cmd.Parameters.AddWithValue("@added_by_name", category.AddedByName);
                cmd.Parameters.AddWithValue("@id", category.Id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                // Сообщение пользователю в случае ошибки
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        /// <summary>
        /// Удаляет категорию из базы данных по указанному идентификатору.
        /// </summary>
        /// <param name="category">Экземпляр <see cref="Category"/> с установленным полем <c>Id</c> для удаления.</param>
        /// <returns>
        /// <c>true</c>, если категория успешно удалена; иначе — <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Выбрасывается, если <paramref name="category"/> равен <c>null</c>.
        /// </exception>
        /// <remarks>
        /// Удаление происходит по первичному ключу категории — <c>Id</c>.
        /// </remarks>
        public bool Delete(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);

            try
            {
                string sql = "DELETE FROM table_categories WHERE id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@id", category.Id);

                conn.Open();
                int rows = cmd.ExecuteNonQuery();

                isSuccess = rows > 0;
            }
            catch (Exception ex)
            {
                // Сообщение пользователю в случае ошибки
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return isSuccess;
        }

        /// <summary>
        /// Ищет категории по ключевому слову или части слова в полях <c>id</c>, <c>title</c> или <c>description</c>.
        /// </summary>
        /// <param name="keywords">Строка для поиска (например, часть названия или описания категории).</param>
        /// <returns>
        /// <see cref="DataTable"/>, содержащая найденные записи, удовлетворяющие условию поиска.
        /// </returns>
        /// <remarks>
        /// Поиск осуществляется по частичному совпадению с <c>id</c>, <c>title</c> и <c>description</c>.
        /// В случае ошибки пользователю выводится сообщение.
        /// В текущей реализации используется конкатенация строки запроса:
        /// для предотвращения SQL-инъекций рекомендуется использовать параметризованные запросы!
        /// </remarks>
        public DataTable Search(string keywords)
        {
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();

            try
            {
                string sql = "SELECT * FROM table_categories WHERE id LIKE '%" + keywords + "%' OR title LIKE '%" + keywords + "%' OR description LIKE '%" + keywords + "%'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                // Сообщение пользователю в случае ошибки
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return dt;
        }
    }
}