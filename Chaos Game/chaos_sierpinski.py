from utils import *
WIN = pygame.display.set_mode((WIDTH, HEIGHT))
pygame.display.set_caption("Chaos Game - Sierpinski Triangle")
clock = pygame.time.Clock()


def chaos_sierpinski():
    CENTER = tuple(x / 2 for x in (WIDTH, HEIGHT))
    tri_vertices = generate_equilateral_triangle(RADIUS)
    tri_vertices = [np.array([x + CENTER[0], CENTER[1] - y]) for (x, y) in tri_vertices]
    DOTS = []

    # Randomly select a corner of the triangle
    current_position = get_random_point_in_triangle(tri_vertices)

    run = True
    while run:
        # Go to the next frame
        clock.tick(FPS)
        # Get all the events that happen this frame
        events = pygame.event.get()
        # Randomly select a corner of the triangle
        corner, _ = get_random_corner(tri_vertices)
        # Move half way to a corner
        current_position = (current_position + corner) / 2
        DOTS += [current_position]

        for event in events:
            if event.type == pygame.QUIT:
                run = False
                break

        draw(WIN, CENTER, tri_vertices, DOTS)
        print(len(DOTS))

    pygame.quit()


def get_random_point_in_triangle(tri_vertices):
    indices = list(range(0, len(tri_vertices)))
    i = np.random.randint(0, len(indices))
    del indices[i]
    v = tri_vertices[i]
    # Randomly generate a point inside the triangle
    v1, v2 = tri_vertices[indices[0]] - v, tri_vertices[indices[1]] - v
    c1 = np.random.uniform(0, 1)
    c2 = np.random.uniform(0, 1 - c1)
    return v + c1 * v1 + c2 * v2


def generate_equilateral_triangle(radius):
    tri_vertices = []
    for i in range(0, 3):
        theta = 90 + i * 120
        x = radius * np.cos(np.radians(theta))
        y = radius * np.sin(np.radians(theta))
        tri_vertices += [(x, y)]
    return tri_vertices


def get_random_corner(tri_vertices):
    indices = list(range(0, len(tri_vertices)))
    i = np.random.randint(0, len(indices))
    del indices[i]
    return tri_vertices[i], indices


def draw(win, center, tri_vertices, dots):
    win.fill(BG_COLOR)

    for dot in dots:
        pygame.draw.circle(win, DOT_COLOR, dot, DOT_RADIUS)

    pygame.draw.circle(win, CENTER_COLOR, center, CENTER_RADIUS)

    for corner in tri_vertices:
        pygame.draw.circle(win, CORNER_COLOR, corner, CORNER_RADIUS)

    pygame.display.update()


def main():
    chaos_sierpinski()


if __name__ == "__main__":
    main()
