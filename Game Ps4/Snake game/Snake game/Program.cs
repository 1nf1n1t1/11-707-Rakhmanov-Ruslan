using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

namespace Snake_game
{
    struct Point
    {
        public int x, y;
        public Point(int x_coordiante, int y_coordinate)
        {
            x = x_coordiante;
            y = y_coordinate;
        }

    }

    struct Turn
    {
        public Point pos;
        public int dir_x, dir_y;

        public Turn(Point turn_position, int direction_x, int direction_y)
        {
            pos = turn_position;
            dir_x = direction_x;
            dir_y = direction_y;
        }

    }

    static class Game
    {
        //Параметры игры
        public static int steps;
        public static int score;
        public static bool game_on;
        public static int time_of_timer;
        public static bool started;

        //Создание элементов игры
        static Input input;
        static Output output;
        static Field field;
        static Timer timer;
        static Snake snake;
        static Apple apple;

        //Начало программы
        static void Main(string[] args)
        {
            //Инициализируем игру
            Init_game();
            //Пока игра включена
            while (game_on)
            {
                //Ждем ввода пользователя
                input.Check_control_keys_in_RT();
                //Если игра началась, врубаем таймер
                if (started)
                    timer.Start();
            }
        }
        //Инициализация игры
        static void Init_game()
        {
            //Настраиваем консоль
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;
            //Игра включена
            game_on = true;
            //Инициализируем то, что не нужно повторно инициализировать при рестарте
            output = new Output();
            input = new Input();
            //Настраиваем таймер
            time_of_timer = 500;
            timer = new Timer(time_of_timer);
            timer.AutoReset = false;
            timer.Elapsed += new ElapsedEventHandler(Time_elapsed_event);
            //Инициализируем элементы игры, которые, возможно, будут "перезапущены"
            Reinit_components();
        }
        //Метод для переинициализации компонентов, т.е. для рестарта
        public static void Reinit_components()
        {
            //Останавливаем таймер
            timer.Stop();
            //Создаем поле, змейку и яблоко
            field = new Field(10, 10);
            snake = new Snake(field, new Point(field.width / 2, field.height / 2));
            apple = new Apple(field, snake);
            //Инициализируем первоначальные значения
            started = false;
            steps = 0;
            score = 0;
            //Отображаем первоначальную картинку
            output.Update(field, steps, score);
            //Рисуем первоначальную информацию
            Console.WriteLine("Управление: "); 
            Console.WriteLine("Стрелки - управление змейкой");
            Console.WriteLine("R - перезапуск");
            
        }
        //Метод, который вызывается, когда истекает время таймера
        static void Time_elapsed_event(object source, ElapsedEventArgs a)
        {
            //Проверяем последнюю нажатую клавишу до истечения времени таймера
            input.Check_control_keys_in_step(snake);
            //Назначаем старой позиции хвоста нынешнюю
            snake.tail_old_position = snake.tail.position;

            //Каждый сегмент змейки делает ход
            for (int i = 0; i < snake.segments.Count; i++)
                snake.segments[i].Move(field, snake);

            //Проверяем, совпадает ли координаты головы змейки и яблока.
            if (Equals(snake.head.position, apple.position))
            {
                //Если да, то заносим это место в список,
                //создаем новое яблоко и добавляем очко 
                snake.eated_places.Add(apple.position);
                apple.Create_apple(field, snake);
                score++;
            }

            //Если на предыдущем шаге "сяъеденное яблоко" достигла хвоста, 
            if (snake.ready_to_make_segment == true)
            {
                //то добавляем сегмент и снимаем ready
                snake.Add_new_segment(field);
                snake.ready_to_make_segment = false;
            }
            //Если
            if (snake.eated_places.Count != 0)
                if (Equals(snake.eated_places[0], snake.tail.position))
                {
                    snake.ready_to_make_segment = true;
                    snake.eated_places.RemoveAt(0);
                }

            //Добавляем кол-во ходов 
            steps++;

            //Проверяем, не врезалась ли змейка в саму себя
            if (snake.Check_collision())
                //Если да, начинаем игру заново
                Game.Reinit_components();
            else
                //Иначе перезапускаем таймер
                timer.Start();

            //Обновляем экран
            output.Update(field, steps, score);

        }
    }

    class Output
    {
        //Метод, обновляющий поле на консоли
        public void Update(Field field, int steps, int score)
        {
            //Чистим консоль
            Console.Clear();

            //"Рисуем" поле заново
            for (int y = 0; y < field.height; y++)
            {
                Console.WriteLine();
                for (int x = 0; x < field.width; x++)
                    Console.Write(field.Field_arr[x, y]);
            }
            //Выводим данные о кол-ве ходов и съеденных яблок
            Console.WriteLine("\nКол-во ходов: " + steps.ToString());
            Console.WriteLine("Яблок съедено: " + score.ToString() + "\n");
        }
    }

    class Input
    {
        //Создаем CKI
        public ConsoleKeyInfo cki;

        //Метод обработки нажатий клавиш, обрабатываемых в настоящем времени
        public void Check_control_keys_in_RT()
        {
            cki = Console.ReadKey(true);
            //В каждом case проверяем, не движется ли змейка противоположно
            //направлению клавиши.
            switch (cki.Key)
            {
                    //При нажатие на клавишу управления змейкой -- игра начинается
                case ConsoleKey.DownArrow:
                    {
                        if (Game.started == false)
                            Game.started = true;
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (Game.started == false)
                            Game.started = true;
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (Game.started == false)
                            Game.started = true;
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if (Game.started == false)
                            Game.started = true;
                        break;
                    }
                    //Выход и рестарт
                case ConsoleKey.Escape:
                    {
                        Game.game_on = false;
                        break;
                    }
                case ConsoleKey.R:
                    {
                        Game.Reinit_components();
                        break;
                    }

            }
        }
        //Метод обработки нажатий клавиш, обрабатываемых каждый ход
        public void Check_control_keys_in_step(Snake snake)
        {
            switch (cki.Key)
            {
                    //При нажатии на стрелку создается поворот на голове змейки.
                    //Причем нельзя создать поворот, противоположный направлению головы
                case ConsoleKey.DownArrow:
                    {
                        if (snake.head.dir_y != -1)
                            snake.turns.Add(new Turn(snake.head.position, 0, 1));
                        break;
                    }
                case ConsoleKey.UpArrow:
                    {
                        if (snake.head.dir_y != 1)
                            snake.turns.Add(new Turn(snake.head.position, 0, -1));
                        break;
                    }
                case ConsoleKey.RightArrow:
                    {
                        if (snake.head.dir_x != -1)
                            snake.turns.Add(new Turn(snake.head.position, 1, 0));
                        break;
                    }
                case ConsoleKey.LeftArrow:
                    {
                        if (snake.head.dir_x != 1)
                            snake.turns.Add(new Turn(snake.head.position, -1, 0));
                        break;
                    }
            }
        }
    }

    class Field
    {
        //Ширина и высота поля включая рамки
        public int width, height;
        //Массив поля
        public char[,] Field_arr;

        //Конструтор/инициализатор поля. Параметры обозначают ширину и длину
        //игрового поля БЕЗ рамок
        public Field(int field_width, int field_height)
        {

            //Инициализируем размеры массива
            width = field_width + 2;
            height = field_height + 2;

            //Инициализируем массив поля
            Field_arr = new char[width, height];

            //Основное поле
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    Field_arr[x, y] = ' ';
            }

            //Рамки
            for (int x = 0; x < width; x++)
            {
                Field_arr[x, height - 1] = '#';
                Field_arr[x, 0] = '#';
            }
            for (int y = 0; y < height; y++)
            {
                Field_arr[0, y] = '#';
                Field_arr[width - 1, y] = '#';
            }
        }
    }

    class Snake
    {
        //Точка для запоминания позиции хвоста. Нужна, чтобы при создании нового сегмента
        //он появился на старом месте последнего сегмента, который уже передвинулся
        public Point tail_old_position;
        //Списки сегментов, поворотов змейки и мест, где съедено яблоко
        public List<Segment> segments;
        public List<Turn> turns;
        public List<Point> eated_places;
        //Флаг eated. Нужен, чтобы на следующем шаге появился новый сегмент
        public bool ready_to_make_segment;
        //Последний сегмент и первый (голова и хвостик)
        public Segment tail;
        public Segment head;

        public Snake(Field field, Point start_position)
        {
            //Инициализируем списки
            turns = new List<Turn>();
            segments = new List<Segment>();
            eated_places = new List<Point>();
            //Создаем и добавляем первый сегмент, т.е. голову змейки
            head = new Segment(start_position, field, '@', 0, 0);
            segments.Add(head);
            //Назначаем последним сегментом первый, т.е. хвостом - голову
            tail = head;
            tail_old_position = tail.position;
        }
        //Метод для добавления к змейке сегмента
        public void Add_new_segment(Field field)
        {
            //Создаем сегмент змейки в в прошлое место хвостика
            Segment new_segment = new Segment(tail_old_position, field, 'o',
                                              tail.dir_x, tail.dir_y);
            //Добавляем этот сегмент в список и назначаем его хвостиком
            segments.Add(new_segment);
            tail = new_segment;
        }
        //Метод для проверки столкновения змейки с самой собой
        public bool Check_collision()
        {
            //Проверяем для каждого сегмента змейки
            //не совпадает ли координаты с головой
            for (int k = 1; k < segments.Count; k++)
                if (Equals(head.position, segments[k].position))
                    return true;
            return false;
        }
    }

    class Segment
    {
        //Позиция сегмента и направление
        public Point position;
        public int dir_x, dir_y;

        //Символ, который рисуется на поле
        public char symbol;

        //Конструтор сегмента
        public Segment(Point respawn_position, Field field,
                       char symb, int direction_x, int direction_y)
        {
            //Задаем начальные параметры
            symbol = symb;
            position = respawn_position;
            dir_x = direction_x;
            dir_y = direction_y;

            //и назначаем сегмент на поле
            field.Field_arr[position.x, position.y] = symbol;
        }

        //Метод, отвечающий за передвижения сегментов по полю
        public void Move(Field field, Snake snake)
        {
            //Назначаем вместо символа клетки, с которой собирается уходить сегмент,
            //новый символ
            Set_char_before_move(snake, field);
            //Вызываем метод, обрабатывающие случай, если сегмент на повороте
            Behaivor_on_turn(snake);
            //Делаем шаг вперед
            position = new Point(position.x + dir_x, position.y + dir_y);
            //Проверяем, не залез ли сегмент на рамку и не находиться ли он на съеденном месте
            Behaivor_on_border(field);
            Switch_char(snake, field);
            //Назначаем сегмент на новое местоположение на поле
            field.Field_arr[position.x, position.y] = symbol;
        }
        //Метод, определяющий, что оставить после себя на клетке поля перед самим передвижением
        void Set_char_before_move(Snake snake, Field field)
        {
            //В основном не иммет значение, что оставят после себя сегменты,
            //т.к. на их место встанут другие сегменты, но за последним хвостом ничего не стоит,
            //поэтому только он изменяет после себя символ на поле
            if (Equals(snake.tail))
            {
                //Бывают случаи, когда голова идет впритык к хвосту.
                //Обычно хвост оставляет за собой ' ', но тут он должен оставить после себя '@',
                //т.к. голова идет первее и иначе ее клетку просто заменится на ' '
                if (Equals(snake.head.position, this.position) && !Equals(snake.head))
                    field.Field_arr[position.x, position.y] = snake.head.symbol;
                //В остальных случаях ставим пустое место
                else
                    field.Field_arr[position.x, position.y] = ' ';
            }
        }
        //Метод, обрабатывающий поведение сегмента на повороте
        void Behaivor_on_turn(Snake snake)
        {
            //Проверяем для каждого поворота, находится ли сегмент змейки на нем
            for (int i = 0; i < snake.turns.Count; i++)
            {
                if (Equals(snake.turns[i].pos, position))
                {
                    //Если да, то меняем направление сегмента соответственно повороту
                    dir_x = snake.turns[i].dir_x;
                    dir_y = snake.turns[i].dir_y;

                    //Если последний сегмент прошелся по повороту -- удаляем его (поворот)
                    if (Equals(snake.turns[i].pos, snake.tail.position))
                        snake.turns.RemoveAt(i);
                }
            }
        }
        //Метод, обрабатывающий поведение сегмента,  он на границе
        void Behaivor_on_border(Field field)
        {
            //Если сегмент зашел на поле, сносим его назад противоположно направлению на
            //ширину или высоту игрового поля без рамок
            if (position.x == 0 || position.x == field.width - 1)
                position.x -= dir_x * (field.width - 2);
            if (position.y == 0 || position.y == field.height - 1)
                position.y -= dir_y * (field.height - 2);
        }
        //Метод, определяющий поведение сегмента на месте съеденного яблока
        void Switch_char(Snake snake, Field field)
        {
            //Для каждого съеденного места проверяем,
            //совпадает ли позиция сегмента (кроме головы) с этим местом
            for (int i = 0; i < snake.eated_places.Count; i++)
            {
                if (!Equals(snake.head))
                {
                    if (Equals(position, snake.eated_places[i]))
                    {
                        //Соответственно меняем символ и заканчиваем цикл
                        symbol = 'O';
                        break;
                    }
                        //иначе стандартный символ 
                    else
                        symbol = 'o';
                }
            }
            //Т.к. после удаления съеденного места итерация цикла не начнется,
            //назначаем хвосту 'o', если нету съеденных мест. 
            if (snake.eated_places.Count == 0 && !Equals(snake.head))
                snake.tail.symbol = 'o';
        }
    }

    class Apple
    {
        //Рандомизатор для координат нового места яблочек
        Random rnd;

        //Координаты яблока, его символ на поле
        public Point position;
        public char symbol;

        //Конструктор
        public Apple(Field field, Snake snake)
        {
            rnd = new Random();
            symbol = '*';
            Create_apple(field, snake);
        }
        //Метод, создающий яблоко в рандомном месте
        public void Create_apple(Field field, Snake snake)
        {
            //Выбираем случайную позицию на поле
            Point new_position = new Point(rnd.Next(field.width - 2) + 1, rnd.Next(field.height - 2) + 1);
            //Проверяем, не совпадает ли она с какой либо позицией сегментов змейки
            for (int i = 0; i < snake.segments.Count; i++)
            {
                if (!Equals(new_position, snake.segments[i].position))
                    //если нет, то присваиваем позиции новое значение
                    position = new_position;
                else
                {
                    //иначе заново вызываем этот метод, не занося в field новый символ
                    Create_apple(field, snake);
                    break;
                }
            }
            //Назначаем на поле яблоко
            field.Field_arr[position.x, position.y] = symbol;
        }
    }
}