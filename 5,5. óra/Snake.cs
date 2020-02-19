using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;

namespace Snake
{
    //enum Field
    //{
    //    Free, OccupiedOrOut, Apple
    //}
    interface IField
    {
        void Accept(Snake s);
        Coord Place { get; }
        void Die();
    }
    class SnakeField : IField
    {
        Snake parent;
        Coord place;
        public SnakeField(Snake _parent, Coord place)
        {
            this.place = place;
            parent = _parent;
        }

        public Coord Place => place;

        public void Accept(Snake s)
        {
            s.Die();
            //System.Console.WriteLine(s + " was killed by" + parent);
        }

        public void Die()
        {
            throw new NotImplementedException();
        }
    }
    class OutField : IField
    {
        private static OutField instance = new OutField();
        public static OutField Instance
        {
            get => instance;
        }
        public void Accept(Snake s)
        {
            s.Die();
            //System.Console.WriteLine(s + " was killed by wall");

        }
        public void Die()
        {
            throw new NotImplementedException();
        }
        private OutField() {}
        public Coord Place => throw new FieldAccessException();
    }
    class FreeField : IField
    {
        private Coord place;
        public void Accept(Snake s)
        {
            s.MoveTo(place);
        }
        public void Die()
        {
            throw new NotImplementedException();
        }
        public Coord Place => place;
        public FreeField(Coord place) {
            this.place = place;
        }
    }
    interface ITickable
    {
        void Tick();
    }
    class AppleField : IField, ITickable
    {
        int life = 20;
        private Game g;
        private Coord place;
        public Coord Place => place;
        public AppleField(Game g, Coord place)
        {
            this.g = g;
            this.place = place;
            g.Register(this);
        }
        public void Accept(Snake s)
        {
            g.RemoveField(this);
            s.Grow(3);
            s.MoveTo(place);
        }
        public void Die()
        {
            g.unRegister(this);
        }
        public void Tick()
        {
            life -= 1;
            if (life == 0) {
                g.RemoveField(this);
            }
        }
        public static AppleField Instance(Game g, Coord place)
        {
            return new AppleField(g, place);
        }
    }
    enum Direction
    {
        Up, Right, Left, Down
    }
    struct Coord
    {
        public readonly int x;
        public readonly int y;
        public Coord(int p1, int p2)
        {
            x = p1;
            y = p2;
        }
        public Coord Get(Direction d)
        {
            if (d == Direction.Right)
                return new Coord(x+1,y);
            if (d == Direction.Up)
                return new Coord(x,y-1);
            if (d == Direction.Left)
                return new Coord(x-1,y);
            if (d == Direction.Down)
                return new Coord(x,y+1);
            return new Coord(-1,-1);
        }
    }
    interface IControl {
        public Direction SnakeDirection { get; set; }
    }
    class Snake : ITickable, IControl
    {
        private LinkedList<Coord> avail;
        private Game game;
        private int cooldown = 0;
        private SnakeField fieldInstance;
        public bool Dead { get; private set; }
        public Direction SnakeDirection { get; set; }
        private void Push(Coord head)
        {
            game[head] = fieldInstance;
            avail.AddFirst(head);
        }
        private void PopTail()
        {
            game[avail.Last.Value] = new FreeField(avail.Last.Value);
            avail.RemoveLast();
        }
        public Snake(Game g, Coord head)
        {
            SnakeDirection = Direction.Down;
            fieldInstance = new SnakeField(this, head);
            g.AddSnake(this);
            avail = new LinkedList<Coord>();
            game = g;
            Push(head);
        }
        public void Tick()
        {
            if (Dead)
            {
                return;
            }
            Coord newHead = avail.First.Value.Get(SnakeDirection);
            game[newHead].Accept(this);
        }

        public void Die()
        {
            Dead = true;
        }
        public void Grow(int v)
        {
            cooldown += v;
        }
        public void MoveTo(Coord place)
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                PopTail();
            }
            Push(place);
        }
    }    
    class Game {
        private IField[,] fields;
        private List<Snake> snakes;
        private readonly int xsize = 20, ysize = 20;
        public Game()
        {
            snakes = new List<Snake>();
            fields = new IField[ysize,xsize];
            for (int i = 0; i < ysize; i++)
            {
                for (int j = 0; j < xsize; j++)
                {
                    Coord place = new Coord(j,i);
                    fields[i,j] = (i+j)%5 == 0 ? (IField)AppleField.Instance(this, place) : (IField)new FreeField(place);
                }
            }
        }
        public bool End
        {
            get => snakes.All(t=>t.Dead);
            set {
                if (value == false)
                    throw new ArgumentException();
                snakes.ForEach(t=>t.Die());
            }
        }
        public void AddSnake(Snake s)
        {
            snakes.Add(s);
            tickables.Add(s);
        }
        public IField this[Coord i]
        {
            get {
                if (i.x >= xsize || i.y >= ysize || i.x < 0 || i.y < 0)
                {
                    return OutField.Instance;
                }
                return fields[i.y,i.x];
            }
            set => fields[i.y,i.x] = value;
        }
        public void Tick()
        {
            // save list, then iterate through
            // tick might unregister itself
            var ticks = new List<ITickable>(tickables);
            foreach(var s in ticks)
            {
                s.Tick();
            }
        }
        public void Draw()
        {
            Console.Clear();
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < ysize + 1; i++)
                s.Append("##");
            s.Append("\n");
            for (int y = 0; y < fields.GetLength(0); y++)
            {
                s.Append("#");
                for (int x = 0; x < fields.GetLength(1); x++)
                {
                    if (fields[y,x] is SnakeField)
                    {
                        s.Append("[]");
                    }
                    else if (fields[y,x] is AppleField)
                    {
                        s.Append("()");
                    }
                    else
                    {
                        s.Append("  ");
                    }
                }
                s.Append("#\n");
            }
            for (int i = 0; i < ysize + 1; i++)
                s.Append("##");
            Console.WriteLine(s);
        }
        List<ITickable> tickables = new List<ITickable>();
        public void Register(ITickable tickable)
        {
            tickables.Add(tickable);
        }
        internal void unRegister(ITickable field)
        {
            tickables.Remove(field);
        }
        internal void RemoveField(IField field)
        {
            field.Die();
            this[field.Place] = new FreeField(field.Place);
        }
    }
    interface IKeyAction
    {
        void OnPress();
    }
    class SnakeSetDirectionAction : IKeyAction
    {
        private Snake snake;
        private Direction direction;
        public SnakeSetDirectionAction(Snake snake, Direction direction)
        {
            this.snake = snake;
            this.direction = direction;
        }
        public void OnPress()
        {
            snake.SnakeDirection = direction;
        }
    }
    class QuitAction : IKeyAction
    {
        private Game game;
        public QuitAction(Game game)
        {
            this.game = game;
        }

        public void OnPress()
        {
            game.End = true;
        }
    }
    class Controller
    {
        private IDictionary<char, IKeyAction> keys
            = new Dictionary<char, IKeyAction>();
        private void AddSnake(char key, Snake snake, Direction direction)
        {
            keys[key] = new SnakeSetDirectionAction(snake, direction);
        }
        public void RegisterQuitKey(Game g, char key)
        {
            keys[key] = new QuitAction(g);
        }
        public void RegisterSnake(Snake snake, string wasd)
        {
            AddSnake(wasd[0], snake, Direction.Up);
            AddSnake(wasd[1], snake, Direction.Left);
            AddSnake(wasd[2], snake, Direction.Down);
            AddSnake(wasd[3], snake, Direction.Right);
        }
        public void HandleKeyPress(char c)
        {
            if (keys.ContainsKey(c))
            {
                keys[c].OnPress();
            }
        }
    }
    class Program
    {
        public Program() {
            Game game = new Game();
            Controller controller = new Controller();
            controller.RegisterSnake(
                new Snake(game, new Coord(3,3)),
                "wasd"
            );
            controller.RegisterSnake(
                new Snake(game, new Coord(5,5)),
                "ijkl"
            );
            controller.RegisterQuitKey(game, 'q');
                //Thread.Sleep(10000);

            while (!game.End)
            {
                game.Draw();
                while (Console.KeyAvailable)
                {
                    char c = Console.ReadKey().KeyChar;
                    controller.HandleKeyPress(c);
                }
                Thread.Sleep(300);
                game.Tick();
            }

        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
